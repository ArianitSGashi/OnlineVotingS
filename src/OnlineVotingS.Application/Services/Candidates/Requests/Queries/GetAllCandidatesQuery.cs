using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidates.Handlers.Queries;

public class GetAllCandidatesHandler : IRequestHandler<GetAllCandidatesQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetAllCandidatesHandler> _logger;

    public GetAllCandidatesHandler(ICandidateRepository candidateRepository, ILogger<GetAllCandidatesHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all candidates: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
