using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class DeleteResultHandler : IRequestHandler<DeleteResultCommand, bool>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<DeleteResultHandler> _logger;

    public DeleteResultHandler(IResultRepository resultRepository, ILogger<DeleteResultHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _resultRepository.ExistsAsync(request.ResultId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Result with ID {request.ResultId} not found.");
            }

            await _resultRepository.DeleteAsync(request.ResultId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the result with ID {ResultId}: {ErrorMessage}", request.ResultId, ex.Message);
            throw;
        }
    }
}