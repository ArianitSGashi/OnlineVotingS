using MediatR;
using FluentResults;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetResultsByElectionIdQuery : IRequest<Result<IEnumerable<ResultEntity>>>
{
    public int ElectionId { get; }

    public GetResultsByElectionIdQuery(int electionId)
    {
        ElectionId = electionId;
    }
}