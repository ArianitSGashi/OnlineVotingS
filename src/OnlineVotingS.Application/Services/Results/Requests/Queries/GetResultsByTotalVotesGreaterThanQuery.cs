using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetResultsByTotalVotesGreaterThanQuery : IRequest<IEnumerable<Result>>
{
    public int Votes { get; }

    public GetResultsByTotalVotesGreaterThanQuery(int votes)
    {
        Votes = votes;
    }
}