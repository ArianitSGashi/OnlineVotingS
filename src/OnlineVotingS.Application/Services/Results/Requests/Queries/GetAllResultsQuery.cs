using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Results.Requests.Queries;

public class GetAllResultsQuery : IRequest<IEnumerable<Result>>
{
}