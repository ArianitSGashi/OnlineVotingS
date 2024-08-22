using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetResultsByElectionIdQuery : IRequest<IEnumerable<Result>>
{
    public int ElectionId { get; }

    public GetResultsByElectionIdQuery(int electionId)
    {
        ElectionId = electionId;
    }
}