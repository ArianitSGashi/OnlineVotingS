using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetCandidatesByMinIncomeQueryHandler : IRequestHandler<GetCandidatesByMinIncomeQuery, Result<IEnumerable<Candidates>>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByMinIncomeQueryHandler> _logger;

    public GetCandidatesByMinIncomeQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByMinIncomeQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Candidates>>> Handle(GetCandidatesByMinIncomeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByMinIncomeAsync(request.MinIncome);
            return Ok(candidates);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidates with minimum income {MinIncome}: {ErrorMessage}", request.MinIncome, ex.Message);
            return new Result<IEnumerable<Candidates>>().WithError(ErrorCodes.CANDIDATE_NOT_FOUND.ToString());
        }
    }
}