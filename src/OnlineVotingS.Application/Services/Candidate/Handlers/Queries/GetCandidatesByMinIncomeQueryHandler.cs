using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetCandidatesByMinIncomeQueryHandler : IRequestHandler<GetCandidatesByMinIncomeQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByMinIncomeQueryHandler> _logger;

    public GetCandidatesByMinIncomeQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByMinIncomeQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetCandidatesByMinIncomeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByMinIncomeAsync(request.MinIncome);
            _logger.LogInformation("Successfully retrieved {Count} candidates with minimum income {MinIncome}.", candidates.Count(), request.MinIncome);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidates with minimum income {MinIncome}: {ErrorMessage}", request.MinIncome, ex.Message);
            throw;
        }
    }
}

