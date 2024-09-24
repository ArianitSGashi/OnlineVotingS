using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByElectionIdQuery : IRequest<Result<IEnumerable<Candidates>>>
{
    public int ElectionId { get;}

    public GetCandidatesByElectionIdQuery(int electionId)
    {
        ElectionId = electionId;
    }
}