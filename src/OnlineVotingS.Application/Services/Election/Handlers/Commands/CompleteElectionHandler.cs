using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Application.Services.Election.Requests.Commands;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class CompleteElectionHandler : IRequestHandler<CompleteElectionCommand, FluentResults.Result<bool>>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly ILogger<CompleteElectionHandler> _logger;

    public CompleteElectionHandler(IElectionRepository electionsRepository, ILogger<CompleteElectionHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _logger = logger;
    }

    public async Task<FluentResults.Result<bool>> Handle(CompleteElectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var election = await _electionsRepository.GetByTitleAsync(request.Title);
            if (election == null)
            {
                var errorMessage = $"Election with title '{request.Title}' not found.";
                _logger.LogError(errorMessage);
                return FluentResults.Result.Fail(errorMessage);
            }

            if (election.Status == ElectionStatus.Completed)
            {
                var errorMessage = $"Election '{request.Title}' is already completed.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Ok(false); 
            }

            if (election.Status == ElectionStatus.Not_Active)
            {
                var errorMessage = $"Election '{request.Title}' is not active and cannot be completed.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Ok(false); 
            }

            election.Status = ElectionStatus.Completed;
            election.UpdatedAt = DateTime.UtcNow;
            await _electionsRepository.UpdateAsync(election);

            _logger.LogInformation("Election '{ElectionTitle}' has been manually completed.", request.Title);
            return FluentResults.Result.Ok(true); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while completing the election");
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
