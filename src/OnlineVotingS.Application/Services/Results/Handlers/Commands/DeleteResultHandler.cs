using static FluentResults.Result;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

    public class DeleteResultHandler : IRequestHandler<DeleteResultCommand, Result>
    {
        private readonly IResultRepository _resultRepository;
        private readonly ILogger<DeleteResultHandler> _logger;

        public DeleteResultHandler(IResultRepository resultRepository, ILogger<DeleteResultHandler> logger)
        {
            _resultRepository = resultRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await _resultRepository.ExistsAsync(request.ResultId);
                if (!exists)
                {
                    var errorMessage = $"Result with ID {request.ResultId} not found.";
                    _logger.LogWarning(errorMessage);
                    return new Result().WithError($"Result with ID {request.ResultId} not found.");
            }

            await _resultRepository.DeleteAsync(request.ResultId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the result with ID {ResultId}", request.ResultId);
                 return new Result().WithError(ErrorCodes.RESULT_DELETION_FAILED.ToString());
        }
    }
    }
