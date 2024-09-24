using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByNameQuery : IRequest<Result<IEnumerable<Candidates>>>
{
    public string FullName { get;} 

    public GetCandidatesByNameQuery(string name)
    {
        FullName = name;
    }
}