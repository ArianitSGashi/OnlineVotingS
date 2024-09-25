using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetActiveElectionsQuery : IRequest<Result<IEnumerable<Elections>>>
{
}