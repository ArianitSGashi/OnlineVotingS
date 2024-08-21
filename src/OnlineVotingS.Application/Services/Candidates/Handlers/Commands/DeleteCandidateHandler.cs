using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidates.Handlers.Commands;

public class DeleteCandidateHandler : IRequestHandler<DeleteCandidateCommand, bool>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<DeleteCandidateHandler> _logger;

    public DeleteCandidateHandler(ICandidateRepository candidateRepository, ILogger<DeleteCandidateHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _candidateRepository.ExistsAsync(request.CandidateId);
            if (!exists)
            {
                _logger.LogWarning("Candidate with ID {CandidateId} not found.", request.CandidateId);
                throw new KeyNotFoundException($"Candidate with ID {request.CandidateId} not found.");
            }

            await _candidateRepository.DeleteAsync(request.CandidateId);

            _logger.LogInformation("Candidate with ID {CandidateId} deleted successfully.", request.CandidateId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the candidate with ID {CandidateId}: {ErrorMessage}", request.CandidateId, ex.Message);
            throw;
        }
    }
}
