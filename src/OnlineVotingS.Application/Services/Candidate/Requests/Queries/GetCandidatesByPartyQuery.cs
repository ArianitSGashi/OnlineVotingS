using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByPartyQuery : IRequest<IEnumerable<Candidates>>
{
    public string Party { get; set; }

    public GetCandidatesByPartyQuery(string party)
    {
        Party = party;
    }
}
