using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetResultByIdQuery : IRequest<Result>
{
    public int ResultId { get; }

    public GetResultByIdQuery(int resultId)
    {
        ResultId = resultId;
    }
}