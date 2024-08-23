using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByNameQuery : IRequest<IEnumerable<Candidates>>
{
    public string FullName { get;} 

    public GetCandidatesByNameQuery(string name)
    {
        FullName = name;
    }
}