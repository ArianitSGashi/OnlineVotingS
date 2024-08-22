using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetAllElectionsQuery : IRequest<IEnumerable<Elections>>
{
}