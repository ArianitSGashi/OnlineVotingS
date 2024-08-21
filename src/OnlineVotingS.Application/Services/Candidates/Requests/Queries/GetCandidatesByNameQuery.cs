using MediatR;
using System.Collections.Generic;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidates.Requests.Queries;

public class GetCandidatesByNameQuery : IRequest<IEnumerable<Candidates>>
{
    public string Name { get; }

    public GetCandidatesByNameQuery(string name)
    {
        Name = name;
    }
}
