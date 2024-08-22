using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Commands;

public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, bool>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<DeleteCandidateCommandHandler> _logger;

    public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository, ILogger<DeleteCandidateCommandHandler> logger)
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

