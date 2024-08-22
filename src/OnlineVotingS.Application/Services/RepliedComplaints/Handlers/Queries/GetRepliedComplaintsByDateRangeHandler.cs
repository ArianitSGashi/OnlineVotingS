using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaints.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Handlers.Queries;

public class GetRepliedComplaintsByDateRangeHandler : IRequestHandler<GetRepliedComplaintsByDateRangeQuery, List<RepliedComplaint>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintRepository;
    private readonly ILogger<GetRepliedComplaintsByDateRangeHandler> _logger;

    public GetRepliedComplaintsByDateRangeHandler(IRepliedComplaintsRepository repliedComplaintRepository, ILogger<GetRepliedComplaintsByDateRangeHandler> logger)
    {
        _repliedComplaintRepository = repliedComplaintRepository;
        _logger = logger;
    }

    public async Task<List<RepliedComplaint>> Handle(GetRepliedComplaintsByDateRangeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintRepository.GetByDateRangeAsync(request.StartDate, request.EndDate);
            if (repliedComplaints == null || !repliedComplaints.Any())
            {
                _logger.LogWarning("No replied complaints found between {StartDate} and {EndDate}.", request.StartDate, request.EndDate);
                return new List<RepliedComplaints>();
            }

            return repliedComplaints;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching replied complaints between {StartDate} and {EndDate}: {ErrorMessage}", request.StartDate, request.EndDate, ex.Message);
            throw;
        }
    }
}