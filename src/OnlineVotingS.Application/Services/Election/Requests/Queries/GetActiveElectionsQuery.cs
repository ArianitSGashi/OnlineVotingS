using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetActiveElectionsQuery : IRequest<IEnumerable<Elections>>
{
}