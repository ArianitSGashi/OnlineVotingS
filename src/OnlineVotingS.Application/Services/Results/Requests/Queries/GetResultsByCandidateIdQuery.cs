using MediatR;
using FluentResults;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetResultsByCandidateIdQuery : IRequest<Result<IEnumerable<ResultEntity>>>
{
    public int CandidateId { get; }

    public GetResultsByCandidateIdQuery(int candidateId)
    {
        CandidateId = candidateId;
    }
}