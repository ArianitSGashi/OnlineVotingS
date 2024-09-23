using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class DeleteVoteCommandHandler : IRequestHandler<DeleteVoteCommand, FluentResults.Result<bool>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<DeleteVoteCommandHandler> _logger;

    public DeleteVoteCommandHandler(IVotesRepository votesRepository, ILogger<DeleteVoteCommandHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<FluentResults.Result<bool>> Handle(DeleteVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _votesRepository.ExistsAsync(request.VoteId);
            if (!exists)
            {
                var errorMessage = $"Vote with ID {request.VoteId} not found.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage); 
            }

            await _votesRepository.DeleteAsync(request.VoteId);
            _logger.LogInformation("Vote with ID {VoteId} deleted successfully.", request.VoteId);
            return FluentResults.Result.Ok(true); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the vote with ID {VoteId}: {ErrorMessage}", request.VoteId, ex.Message);
            return  FluentResults.Result.Fail(new ExceptionalError(ex)); 
        }
    }
}
