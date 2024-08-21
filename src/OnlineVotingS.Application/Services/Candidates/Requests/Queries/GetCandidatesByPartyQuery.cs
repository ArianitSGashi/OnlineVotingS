using MediatR;
using System.Collections.Generic;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidates.Requests.Queries;

public class GetCandidatesByPartyQuery : IRequest<IEnumerable<Candidates>>
{
    public int PartyId { get; }

    public GetCandidatesByPartyQuery(string party)
    {
        Party = party;
    }
}
