using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;

public class GetResultByIdHandler : IRequestHandler<GetResultByIdQuery, Result<ResultEntity>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetResultByIdHandler> _logger;

    public GetResultByIdHandler(IResultRepository resultRepository, ILogger<GetResultByIdHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<Result<ResultEntity>> Handle(GetResultByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _resultRepository.GetByIdAsync(request.ResultId);
            if (result == null)
            {
                return new Result<ResultEntity>().WithError(ErrorCodes.RESULT_NOT_FOUND.ToString());
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the result with ID {ResultId}: {ErrorMessage}", request.ResultId, ex.Message);
            return new Result<ResultEntity>().WithError(ErrorCodes.RESULT_NOT_FOUND.ToString());
        }
    }
}