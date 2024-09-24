using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class DeleteElectionsHandler : IRequestHandler<DeleteElectionsCommand, Result<bool>>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly ILogger<DeleteElectionsHandler> _logger;

    public DeleteElectionsHandler(IElectionRepository electionsRepository, ILogger<DeleteElectionsHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteElectionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _electionsRepository.ExistsAsync(request.ElectionId);
            if (!exists)
            {
                return new Result<bool>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
            }

            await _electionsRepository.DeleteAsync(request.ElectionId);
            return Ok(true); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the election with ID '{request.ElectionId}': {ex.Message}");
            return new Result<bool>().WithError(ErrorCodes.ELECTION_DELETION_FAILED.ToString());
        }
    }
}
