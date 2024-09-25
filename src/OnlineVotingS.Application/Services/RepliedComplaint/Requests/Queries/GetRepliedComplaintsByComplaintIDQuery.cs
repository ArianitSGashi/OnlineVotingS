using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetRepliedComplaintsByComplaintIDQuery : IRequest<Result<IEnumerable<RepliedComplaints>>>
{
    public int ComplaintID { get;}

    public GetRepliedComplaintsByComplaintIDQuery(int complaintID)
    {
        ComplaintID = complaintID;
    }
}