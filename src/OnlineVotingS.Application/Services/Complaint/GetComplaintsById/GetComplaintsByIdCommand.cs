using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.GetComplaintsById;

public class GetComplaintsByIdCommand : IRequest<Complaints>
{
    public int ComplaintId { get; set; }
}
