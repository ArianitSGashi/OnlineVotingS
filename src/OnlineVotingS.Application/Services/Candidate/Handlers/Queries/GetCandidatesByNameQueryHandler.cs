using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetCandidatesByNameQueryHandler : IRequestHandler<GetCandidatesByNameQuery, Result<IEnumerable<Candidates>>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByNameQueryHandler> _logger;

    public GetCandidatesByNameQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByNameQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Candidates>>> Handle(GetCandidatesByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByNameAsync(request.FullName);
            return Ok(candidates);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidates with name {FullName}: {ErrorMessage}", request.FullName, ex.Message);
            return new Result<IEnumerable<Candidates>>().WithError(ErrorCodes.CANDIDATE_NOT_FOUND.ToString());
        }
    }
}