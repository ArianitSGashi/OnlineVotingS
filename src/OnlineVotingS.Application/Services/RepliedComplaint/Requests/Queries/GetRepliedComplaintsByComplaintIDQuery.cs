using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetRepliedComplaintsByComplaintIDQuery : IRequest<IEnumerable<RepliedComplaints>>
{
    public int ComplaintID { get;}

    public GetRepliedComplaintsByComplaintIDQuery(int complaintID)
    {
        ComplaintID = complaintID;
    }
}