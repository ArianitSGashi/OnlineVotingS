using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidates.Handlers.Queries;

public class GetCandidatesByMinIncomeHandler : IRequestHandler<GetCandidatesByMinIncomeQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByMinIncomeHandler> _logger;

    public GetCandidatesByMinIncomeHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByMinIncomeHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetCandidatesByMinIncomeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByMinIncomeAsync(request.MinIncome);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching candidates with minimum income {MinIncome}: {ErrorMessage}", request.MinIncome, ex.Message);
            throw;
        }
    }
}
