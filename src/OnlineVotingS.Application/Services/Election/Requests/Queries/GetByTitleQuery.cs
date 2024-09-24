using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetByTitleQuery : IRequest<Result<IEnumerable<Elections>>>
{
    public string Title { get; }

    public GetByTitleQuery(string title)
    {
        Title = title;
    }
}