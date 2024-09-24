using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;

public class GetResultsByElectionIdHandler : IRequestHandler<GetResultsByElectionIdQuery, Result<IEnumerable<ResultEntity>>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetResultsByElectionIdHandler> _logger;

    public GetResultsByElectionIdHandler(IResultRepository resultRepository, ILogger<GetResultsByElectionIdHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ResultEntity>>> Handle(GetResultsByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _resultRepository.GetByElectionIdAsync(request.ElectionId);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching results for election ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            return new Result<IEnumerable<ResultEntity>>().WithError(ErrorCodes.RESULT_NOT_FOUND.ToString());
        }
    }
}