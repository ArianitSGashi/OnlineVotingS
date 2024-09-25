using FluentResults;
using static FluentResults.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class CompleteElectionHandler : IRequestHandler<CompleteElectionCommand, Result<bool>>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly ILogger<CompleteElectionHandler> _logger;

    public CompleteElectionHandler(IElectionRepository electionsRepository, ILogger<CompleteElectionHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(CompleteElectionCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Title))
        {
            return new Result<bool>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
        }

        try
        {
            var election = await _electionsRepository.GetByTitleAsync(request.Title);
            if (election == null)
            {
                return new Result<bool>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
            }

            if (election.Status == ElectionStatus.Completed)
            {
                return new Result<bool>().WithError(ErrorCodes.ELECTION_ALREADY_COMPLETED.ToString());
            }

            if (election.Status == ElectionStatus.Not_Active)
            {
                return new Result<bool>().WithError(ErrorCodes.ELECTION_NOT_ACTIVE.ToString());
            }

            election.Status = ElectionStatus.Completed;
            election.UpdatedAt = DateTime.UtcNow;
            await _electionsRepository.UpdateAsync(election);

            return Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while completing the election");
            return new Result<bool>().WithError(ErrorCodes.ELECTION_COMPLETION_FAILED.ToString());
        }
    }
}
