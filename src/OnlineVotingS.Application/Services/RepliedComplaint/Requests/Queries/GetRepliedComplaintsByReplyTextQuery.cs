using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
public class GetRepliedComplaintsByReplyTextQuery : IRequest<Result<IEnumerable<RepliedComplaints>>>
{
    public string ReplyText { get; }

    public GetRepliedComplaintsByReplyTextQuery(string replyText)
    {
        ReplyText = replyText;
    }
}