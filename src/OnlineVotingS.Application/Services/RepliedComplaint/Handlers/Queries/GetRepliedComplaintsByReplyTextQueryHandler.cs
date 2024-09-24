using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetRepliedComplaintsByReplyTextQueryHandler : IRequestHandler<GetRepliedComplaintsByReplyTextQuery, Result<IEnumerable<RepliedComplaints>>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetRepliedComplaintsByReplyTextQueryHandler> _logger;

    public GetRepliedComplaintsByReplyTextQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRepliedComplaintsByReplyTextQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<RepliedComplaints>>> Handle(GetRepliedComplaintsByReplyTextQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintsRepository.GetByReplyTextAsync(request.ReplyText);
            return Ok(repliedComplaints);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving replied complaints by reply text '{ReplyText}': {ErrorMessage}", request.ReplyText, ex.Message);
            return new Result<IEnumerable<RepliedComplaints>>().WithError(ErrorCodes.REPLIED_COMPLAINT_NOT_FOUND.ToString());
        }
    }
}