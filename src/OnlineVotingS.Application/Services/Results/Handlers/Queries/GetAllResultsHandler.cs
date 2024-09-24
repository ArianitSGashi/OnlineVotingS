using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;

public class GetAllResultsHandler : IRequestHandler<GetAllResultsQuery, Result<IEnumerable<ResultEntity>>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetAllResultsHandler> _logger;

    public GetAllResultsHandler(IResultRepository resultRepository, ILogger<GetAllResultsHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ResultEntity>>> Handle(GetAllResultsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _resultRepository.GetAllAsync();
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all results: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<ResultEntity>>().WithError(ErrorCodes.RESULT_NOT_FOUND.ToString());
        }
    }
}