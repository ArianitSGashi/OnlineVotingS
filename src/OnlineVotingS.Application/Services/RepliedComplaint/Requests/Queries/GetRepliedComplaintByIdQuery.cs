using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetRepliedComplaintByIdQuery : IRequest<RepliedComplaints>
{
    public int RepliedComplaintId { get;}

    public GetRepliedComplaintByIdQuery(int repliedComplaintId)
    {
        RepliedComplaintId = repliedComplaintId;
    }
}