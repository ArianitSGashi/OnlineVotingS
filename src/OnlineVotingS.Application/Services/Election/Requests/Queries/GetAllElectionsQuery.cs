using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetAllElectionsQuery : IRequest<Result<IEnumerable<Elections>>>
{
}