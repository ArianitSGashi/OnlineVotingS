using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetCandidatesByPartyQueryHandler : IRequestHandler<GetCandidatesByPartyQuery, Result<IEnumerable<Candidates>>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByPartyQueryHandler> _logger;

    public GetCandidatesByPartyQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByPartyQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Candidates>>> Handle(GetCandidatesByPartyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByPartyAsync(request.PartyName);
            return Ok(candidates);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidates for party {PartyName}: {ErrorMessage}", request.PartyName, ex.Message);
            return new Result<IEnumerable<Candidates>>().WithError(ErrorCodes.CANDIDATE_NOT_FOUND.ToString());
        }
    }
}