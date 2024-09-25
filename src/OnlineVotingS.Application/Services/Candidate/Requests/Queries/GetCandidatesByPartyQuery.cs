using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByPartyQuery : IRequest<Result<IEnumerable<Candidates>>>
{
    public string PartyName { get; } 

    public GetCandidatesByPartyQuery(string party)
    {
        PartyName = party;
    }
}