using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetRepliedComplaintsByComplaintIDQuery : IRequest<IEnumerable<RepliedComplaints>>
{
    public int ComplaintID { get; set; }

    public GetRepliedComplaintsByComplaintIDQuery(int complaintID)
    {
        ComplaintID = complaintID;
    }
}