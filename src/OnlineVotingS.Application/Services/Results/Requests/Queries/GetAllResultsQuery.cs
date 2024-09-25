using MediatR;
using FluentResults;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetAllResultsQuery : IRequest<Result<IEnumerable<ResultEntity>>>
{
}