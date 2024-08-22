using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaints.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Handlers.Queries;

public class GetRepliedComplaintsByUserIdHandler : IRequestHandler<GetRepliedComplaintsByUserIdQuery, List<RepliedComplaint>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintRepository;
    private readonly ILogger<GetRepliedComplaintsByUserIdHandler> _logger;

    public GetRepliedComplaintsByUserIdHandler(IRepliedComplaintsRepository repliedComplaintRepository, ILogger<GetRepliedComplaintsByUserIdHandler> logger)
    {
        _repliedComplaintRepository = repliedComplaintRepository;
        _logger = logger;
    }

    public async Task<List<RepliedComplaints>> Handle(GetRepliedComplaintsByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintRepository.GetByUserIdAsync(request.UserId);
            if (repliedComplaints == null || !repliedComplaints.Any())
            {
                _logger.LogWarning("No replied complaints found for user with ID {UserId}.", request.UserId);
                return new List<RepliedComplainst>();
            }

            return repliedComplaints;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching replied complaints for user with ID {UserId}: {ErrorMessage}", request.UserId, ex.Message);
            throw;
        }
    }

    Task<List<RepliedComplaint>> IRequestHandler<GetRepliedComplaintsByUserIdQuery, List<RepliedComplaint>>.Handle(GetRepliedComplaintsByUserIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}