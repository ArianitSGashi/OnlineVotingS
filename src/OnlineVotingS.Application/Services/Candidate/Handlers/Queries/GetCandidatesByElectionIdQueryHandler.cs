using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetCandidatesByElectionIdQueryHandler : IRequestHandler<GetCandidatesByElectionIdQuery, Result<IEnumerable<Candidates>>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByElectionIdQueryHandler> _logger;

    public GetCandidatesByElectionIdQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByElectionIdQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Candidates>>> Handle(GetCandidatesByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByElectionIdAsync(request.ElectionId);
            return Ok(candidates);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidates for Election ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            return new Result<IEnumerable<Candidates>>().WithError(ErrorCodes.CANDIDATE_NOT_FOUND.ToString());
        }
    }
}