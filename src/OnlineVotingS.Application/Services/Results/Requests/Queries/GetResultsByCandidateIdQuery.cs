using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetResultsByCandidateIdQuery : IRequest<IEnumerable<Result>>
{
    public int CandidateId { get; }

    public GetResultsByCandidateIdQuery(int candidateId)
    {
        CandidateId = candidateId;
    }
}