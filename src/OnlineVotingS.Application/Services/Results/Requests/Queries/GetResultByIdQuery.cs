using MediatR;
using FluentResults;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetResultByIdQuery : IRequest<Result<ResultEntity>>
{
    public int ResultId { get; }

    public GetResultByIdQuery(int resultId)
    {
        ResultId = resultId;
    }
}