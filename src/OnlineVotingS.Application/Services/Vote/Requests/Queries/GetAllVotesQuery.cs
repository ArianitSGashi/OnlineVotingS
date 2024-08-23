using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetAllVotesQuery : IRequest<IEnumerable<Votes>>
{
}