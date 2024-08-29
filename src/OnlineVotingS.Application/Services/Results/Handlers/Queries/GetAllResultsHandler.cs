using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;

public class GetAllResultsHandler : IRequestHandler<GetAllResultsQuery, IEnumerable<Result>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetAllResultsHandler> _logger;

    public GetAllResultsHandler(IResultRepository resultRepository, ILogger<GetAllResultsHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Result>> Handle(GetAllResultsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _resultRepository.GetAllAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all results: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
