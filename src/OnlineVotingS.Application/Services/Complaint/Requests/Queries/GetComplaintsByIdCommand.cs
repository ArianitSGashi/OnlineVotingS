using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetComplaintsByIdCommand : IRequest<Complaints>
{
    public int ComplaintId { get;}

    public GetComplaintsByIdCommand(int complaintId)
    {
        ComplaintId = complaintId;
    }
}