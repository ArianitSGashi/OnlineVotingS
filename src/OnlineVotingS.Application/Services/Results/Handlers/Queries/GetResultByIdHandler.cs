using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;
{
public class GetResultByIdHandler : IRequestHandler<GetResultByIdQuery, Result>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetResultByIdHandler> _logger;

    public GetResultByIdHandler(IResultRepository resultRepository, ILogger<GetResultByIdHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(GetResultByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _resultRepository.GetByIdAsync(request.ResultId);
            if (result == null)
            {
                throw new KeyNotFoundException($"Result with ID {request.ResultId} not found.");
            }

            return result;
        }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching the result with ID {ResultId}: {ErrorMessage}", request.ResultId, ex.Message);
                throw;
            }
        }
    }
}
