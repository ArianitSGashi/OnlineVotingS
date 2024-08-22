using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetRepliedComplaintsByComplaintIDQueryHandler : IRequestHandler<GetRepliedComplaintsByComplaintIDQuery, IEnumerable<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetRepliedComplaintsByComplaintIDQueryHandler> _logger;

    public GetRepliedComplaintsByComplaintIDQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRepliedComplaintsByComplaintIDQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RepliedComplaints>> Handle(GetRepliedComplaintsByComplaintIDQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repliedComplaintsRepository.GetByComplaintIDAsync(request.ComplaintID);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving replied complaints for complaint ID {ComplaintID}: {ErrorMessage}", request.ComplaintID, ex.Message);
            throw;
        }
    }
}