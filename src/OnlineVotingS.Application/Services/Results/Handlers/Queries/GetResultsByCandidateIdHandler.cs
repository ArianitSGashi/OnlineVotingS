using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;

public class GetResultsByCandidateIdHandler : IRequestHandler<GetResultsByCandidateIdQuery, Result<IEnumerable<ResultEntity>>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetResultsByCandidateIdHandler> _logger;

    public GetResultsByCandidateIdHandler(IResultRepository resultRepository, ILogger<GetResultsByCandidateIdHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ResultEntity>>> Handle(GetResultsByCandidateIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _resultRepository.GetByCandidateIdAsync(request.CandidateId);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching results for candidate ID {CandidateId}: {ErrorMessage}", request.CandidateId, ex.Message);
            return new Result<IEnumerable<ResultEntity>>().WithError(ErrorCodes.RESULT_NOT_FOUND.ToString());
        }
    }
}