using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByPartyQuery : IRequest<IEnumerable<Candidates>>
{
    public string PartyName { get; } 

    public GetCandidatesByPartyQuery(string party)
    {
        PartyName = party;
    }
}