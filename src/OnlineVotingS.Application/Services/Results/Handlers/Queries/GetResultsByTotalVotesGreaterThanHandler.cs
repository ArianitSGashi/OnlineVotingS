using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;

public class GetResultsByTotalVotesGreaterThanHandler : IRequestHandler<GetResultsByTotalVotesGreaterThanQuery, Result<IEnumerable<ResultEntity>>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetResultsByTotalVotesGreaterThanHandler> _logger;

    public GetResultsByTotalVotesGreaterThanHandler(IResultRepository resultRepository, ILogger<GetResultsByTotalVotesGreaterThanHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ResultEntity>>> Handle(GetResultsByTotalVotesGreaterThanQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _resultRepository.GetByTotalVotesGreaterThanAsync(request.Votes);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching results with total votes greater than {Votes}: {ErrorMessage}", request.Votes, ex.Message);
            return new Result<IEnumerable<ResultEntity>>().WithError(ErrorCodes.RESULT_NOT_FOUND.ToString());
        }
    }
}