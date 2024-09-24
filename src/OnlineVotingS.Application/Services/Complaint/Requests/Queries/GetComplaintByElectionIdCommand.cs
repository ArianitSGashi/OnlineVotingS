using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetComplaintByElectionIdCommand : IRequest<Result<IEnumerable<Complaints>>>
{
    public int ElectionId { get;}

    public GetComplaintByElectionIdCommand(int electionId)
    {
        ElectionId = electionId;
    }
}