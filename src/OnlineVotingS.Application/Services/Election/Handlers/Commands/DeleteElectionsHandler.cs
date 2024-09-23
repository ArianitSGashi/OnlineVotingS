using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class DeleteElectionsHandler : IRequestHandler<DeleteElectionsCommand, FluentResults.Result<bool>>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly ILogger<DeleteElectionsHandler> _logger;

    public DeleteElectionsHandler(IElectionRepository electionsRepository, ILogger<DeleteElectionsHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _logger = logger;
    }

    public async Task<FluentResults.Result<bool>> Handle(DeleteElectionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _electionsRepository.ExistsAsync(request.ElectionId);
            if (!exists)
            {
                var errorMessage = $"Elections with ID {request.ElectionId} not found.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage); 
            }

            await _electionsRepository.DeleteAsync(request.ElectionId);
            _logger.LogInformation("Election with ID {ElectionId} successfully deleted.", request.ElectionId);
            return FluentResults.Result.Ok(true); 
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the election with ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex)); 
        }
    }
}
