using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Errors;
using OnlineVotingS.Domain.Interfaces;
using static FluentResults.Result;


namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class DeleteVoteCommandHandler : IRequestHandler<DeleteVoteCommand, Result<bool>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<DeleteVoteCommandHandler> _logger;

    public DeleteVoteCommandHandler(IVotesRepository votesRepository, ILogger<DeleteVoteCommandHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _votesRepository.ExistsAsync(request.VoteId);
            if (!exists)
            {
                var errorMessage = $"Vote with ID {request.VoteId} not found.";
                _logger.LogWarning(errorMessage);
                return Result.Fail(ErrorCodes.RESULT_NOT_FOUND.ToString());
            }

            await _votesRepository.DeleteAsync(request.VoteId);
            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the vote with ID {VoteId}: {ErrorMessage}", request.VoteId, ex.Message);
            return Result.Fail(ErrorCodes.VOTE_DELETION_FAILED.ToString());
        }
    }
}
