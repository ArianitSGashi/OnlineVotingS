using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaints.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Handlers.Queries;

public class GetAllRepliedComplaintsHandler : IRequestHandler<GetAllRepliedComplaints, List<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintRepository;
    private readonly ILogger<GetAllRepliedComplaintsHandler> _logger;

    public GetAllRepliedComplaintsHandler(IRepliedComplaintsRepository repliedComplaintRepository, ILogger<GetAllRepliedComplaintsHandler> logger)
    {
        _repliedComplaintRepository = repliedComplaintRepository;
        _logger = logger;
    }

    public async Task<List<RepliedComplaints>> Handle(GetAllRepliedComplaints request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintRepository.GetAllAsync();
            if (repliedComplaints == null || !repliedComplaints.Any())
            {
                _logger.LogWarning("No replied complaints found.");
                return new List<RepliedComplaints>();
            }

            return repliedComplaints;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all replied complaints: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}