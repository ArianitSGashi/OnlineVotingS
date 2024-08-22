using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;

public class GetResultsByElectionIdHandler : IRequestHandler<GetResultsByElectionIdQuery, IEnumerable<Result>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetResultsByElectionIdHandler> _logger;

    public GetResultsByElectionIdHandler(IResultRepository resultRepository, ILogger<GetResultsByElectionIdHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Result>> Handle(GetResultsByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _resultRepository.GetByElectionIdAsync(request.ElectionId);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching results for election ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            throw;
        }
    }
}