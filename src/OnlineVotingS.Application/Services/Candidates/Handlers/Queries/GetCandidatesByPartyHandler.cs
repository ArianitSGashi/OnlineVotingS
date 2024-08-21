using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidates.Handlers.Queries;

public class GetCandidatesByPartyHandler : IRequestHandler<GetCandidatesByPartyQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByPartyHandler> _logger;

    public GetCandidatesByPartyHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByPartyHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetCandidatesByPartyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByPartyIdAsync(request.Party);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching candidates for Party ID {PartyId}: {ErrorMessage}", request.Party, ex.Message);
            throw;
        }
    }
}
