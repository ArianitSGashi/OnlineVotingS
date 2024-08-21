using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidates.Requests.Queries;

public class GetCandidateByIdQuery : IRequest<Candidates>
{
    public int CandidateId { get; }

    public GetCandidateByIdQuery(int candidateId)
    {
        CandidateId = candidateId;
    }
}
