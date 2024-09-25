using static FluentResults.Result;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class DeleteResultHandler : IRequestHandler<DeleteResultCommand, Result<bool>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<DeleteResultHandler> _logger;

    public DeleteResultHandler(IResultRepository resultRepository, ILogger<DeleteResultHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _resultRepository.ExistsAsync(request.ResultId);
            if (!exists)
            {
                return new Result<bool>().WithError($"Result with ID {request.ResultId} not found.");
            }

            await _resultRepository.DeleteAsync(request.ResultId);
            return Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the result with ID {ResultId}", request.ResultId);
            return new Result<bool>().WithError(ErrorCodes.RESULT_DELETION_FAILED.ToString());
        }
    }
}