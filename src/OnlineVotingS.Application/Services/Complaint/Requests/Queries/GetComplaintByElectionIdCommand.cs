using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetComplaintByElectionIdCommand : IRequest<IEnumerable<Complaints>>
{
    public int ElectionId { get; set; }

    public GetComplaintByElectionIdCommand(int electionId)
    {
        ElectionId = electionId;
    }
}
