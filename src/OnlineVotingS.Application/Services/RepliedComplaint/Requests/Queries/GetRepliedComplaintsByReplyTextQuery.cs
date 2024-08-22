using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
public class GetRepliedComplaintsByReplyTextQuery : IRequest<IEnumerable<RepliedComplaints>>
{
    public string ReplyText { get; }

    public GetRepliedComplaintsByReplyTextQuery(string replyText)
    {
        ReplyText = replyText;
    }
}