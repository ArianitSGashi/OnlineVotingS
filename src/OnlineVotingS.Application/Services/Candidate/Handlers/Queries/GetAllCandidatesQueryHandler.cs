using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, Result<IEnumerable<Candidates>>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetAllCandidatesQueryHandler> _logger;

    public GetAllCandidatesQueryHandler(ICandidateRepository candidateRepository, ILogger<GetAllCandidatesQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Candidates>>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return Ok(candidates);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all candidates: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<Candidates>>().WithError(ErrorCodes.CANDIDATE_NOT_FOUND.ToString());
        }
    }
}