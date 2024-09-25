using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetRepliedComplaintsByComplaintIDQueryHandler : IRequestHandler<GetRepliedComplaintsByComplaintIDQuery, Result<IEnumerable<RepliedComplaints>>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetRepliedComplaintsByComplaintIDQueryHandler> _logger;

    public GetRepliedComplaintsByComplaintIDQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRepliedComplaintsByComplaintIDQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<RepliedComplaints>>> Handle(GetRepliedComplaintsByComplaintIDQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintsRepository.GetByComplaintIDAsync(request.ComplaintID);
            return Ok(repliedComplaints);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving replied complaints for complaint ID {ComplaintID}: {ErrorMessage}", request.ComplaintID, ex.Message);
            return new Result<IEnumerable<RepliedComplaints>>().WithError(ErrorCodes.REPLIED_COMPLAINT_NOT_FOUND.ToString());
        }
    }
}