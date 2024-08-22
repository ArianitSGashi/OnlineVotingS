using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class DeleteElectionsHandler : IRequestHandler<DeleteElectionsCommand, bool>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly ILogger<DeleteElectionsHandler> _logger;

    public DeleteElectionsHandler(IElectionRepository electionsRepository, ILogger<DeleteElectionsHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteElectionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _electionsRepository.ExistsAsync(request.ElectionId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Elections with ID {request.ElectionId} not found.");
            }

            await _electionsRepository.DeleteAsync(request.ElectionId);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the elections with ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            throw;
        }
    }
}