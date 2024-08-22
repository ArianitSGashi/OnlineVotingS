using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetByTitleQuery : IRequest<IEnumerable<Elections>>
{
    public string Title { get; }

    public GetByTitleQuery(string title)
    {
        Title = title;
    }
}