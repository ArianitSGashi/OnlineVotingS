using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidateByIdQuery : IRequest<Result<Candidates>>
{
    public int CandidateId { get;}

    public GetCandidateByIdQuery(int candidateId)
    {
        CandidateId = candidateId;
    }
}