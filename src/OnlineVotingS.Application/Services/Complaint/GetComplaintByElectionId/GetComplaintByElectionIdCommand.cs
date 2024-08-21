using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.GetComplaintByElectionId;

public class GetComplaintByElectionIdCommand : IRequest<IEnumerable<Complaints>>
{
    public int ElectionId { get; set; }
}
