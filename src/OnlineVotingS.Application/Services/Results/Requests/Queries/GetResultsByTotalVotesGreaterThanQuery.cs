using MediatR;
using FluentResults;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetResultsByTotalVotesGreaterThanQuery : IRequest<Result<IEnumerable<ResultEntity>>>
{
    public int Votes { get; }

    public GetResultsByTotalVotesGreaterThanQuery(int votes)
    {
        Votes = votes;
    }
}