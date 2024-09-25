using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetAllVotesQuery : IRequest<Result<IEnumerable<Votes>>>
{
}