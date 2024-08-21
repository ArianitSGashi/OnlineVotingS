using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidates.Handlers.Queries;

public class GetCandidateByIdHandler : IRequestHandler<GetCandidateByIdQuery, Candidates>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidateByIdHandler> _logger;

    public GetCandidateByIdHandler(ICandidateRepository candidateRepository, ILogger<GetCandidateByIdHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Candidates> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);
            if (candidate == null)
            {
                _logger.LogWarning("Candidate with ID {CandidateId} not found.", request.CandidateId);
                throw new KeyNotFoundException($"Candidate with ID {request.CandidateId} not found.");
            }

            return candidate;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the candidate with ID {CandidateId}: {ErrorMessage}", request.CandidateId, ex.Message);
            throw;
        }
    }
}
