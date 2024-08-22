using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetUpcomingElectionsQuery : IRequest<IEnumerable<Elections>>
{
    public DateTime StartDate { get; }

    public GetUpcomingElectionsQuery(DateTime startdate)
    {
        StartDate = startdate;
    }
}