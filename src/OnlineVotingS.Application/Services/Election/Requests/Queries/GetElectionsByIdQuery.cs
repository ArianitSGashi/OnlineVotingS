using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetElectionsByIdQuery : IRequest<Elections>
{
    public int ElectionID { get; }

    public GetElectionsByIdQuery(int electionId)
    {
        ElectionID = electionId;
    }
}