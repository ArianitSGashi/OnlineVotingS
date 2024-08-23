using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetAllCandidatesQuery : IRequest<IEnumerable<Candidates>>
{
}