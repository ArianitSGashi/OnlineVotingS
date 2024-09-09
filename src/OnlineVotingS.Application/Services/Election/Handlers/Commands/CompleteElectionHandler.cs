using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Application.Services.Election.Requests.Commands;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class CompleteElectionHandler : IRequestHandler<CompleteElectionCommand, bool>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly ILogger<CompleteElectionHandler> _logger;

    public CompleteElectionHandler(IElectionRepository electionsRepository, ILogger<CompleteElectionHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(CompleteElectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var election = await _electionsRepository.GetByTitleAsync(request.Title);
            if (election == null)
            {
                throw new KeyNotFoundException($"Election with title '{request.Title}' not found.");
            }

            if (election.Status == ElectionStatus.Completed)
            {
                return false; // Election is already completed
            }

            election.Status = ElectionStatus.Completed;
            election.UpdatedAt = DateTime.UtcNow;

            await _electionsRepository.UpdateAsync(election);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while completing the election with title {ElectionTitle}: {ErrorMessage}", request.Title, ex.Message);
            throw;
        }
    }
}