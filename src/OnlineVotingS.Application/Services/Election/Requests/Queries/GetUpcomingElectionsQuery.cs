using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetUpcomingElectionsQuery : IRequest<Result<IEnumerable<Elections>>>
{
    public DateTime StartDate { get; }

    public GetUpcomingElectionsQuery(DateTime startdate)
    {
        StartDate = startdate;
    }
}