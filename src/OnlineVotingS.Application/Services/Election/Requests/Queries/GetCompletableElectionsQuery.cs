using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetCompletableElectionsQuery : IRequest<Result<IEnumerable<Elections>>>
{
}