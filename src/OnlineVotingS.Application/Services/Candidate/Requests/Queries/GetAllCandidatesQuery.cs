using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetAllCandidatesQuery : IRequest<Result<IEnumerable<Candidates>>>
{
}