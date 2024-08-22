using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByNameQuery : IRequest<IEnumerable<Candidates>>
{
    public string Name { get; set; }

    public GetCandidatesByNameQuery(string name)
    {
        Name = name;
    }
}
