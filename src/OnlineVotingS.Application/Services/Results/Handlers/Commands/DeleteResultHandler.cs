using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class DeleteResultHandler : IRequestHandler<DeleteResultCommand, FluentResults.Result>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<DeleteResultHandler> _logger;

    public DeleteResultHandler(IResultRepository resultRepository, ILogger<DeleteResultHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<FluentResults.Result> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _resultRepository.ExistsAsync(request.ResultId);
            if (!exists)
            {
                var errorMessage = $"Result with ID {request.ResultId} not found.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage);  
            }

            await _resultRepository.DeleteAsync(request.ResultId);
            _logger.LogInformation("Result with ID {ResultId} was successfully deleted.", request.ResultId);
            return FluentResults.Result.Ok();  
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the result with ID {ResultId}", request.ResultId);
            return FluentResults.Result.Fail(new ExceptionalError(ex));  
        }
    }
}
