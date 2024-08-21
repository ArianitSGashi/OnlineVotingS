using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidates.Handlers.Queries;

public class GetCandidatesByElectionIdHandler : IRequestHandler<GetCandidatesByElectionIdQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByElectionIdHandler> _logger;

    public GetCandidatesByElectionIdHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByElectionIdHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetCandidatesByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByElectionIdAsync(request.ElectionId);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching candidates for Election ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            throw;
        }
    }
}
