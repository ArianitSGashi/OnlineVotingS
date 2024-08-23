using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetRecentVotesQuery : IRequest<IEnumerable<Votes>>
{
      public DateTime Date { get; }

      public GetRecentVotesQuery(DateTime date)
      {
            Date = date;
      }
}