using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetActiveElectionsHandler : IRequestHandler<GetActiveElectionsQuery, IEnumerable<Elections>>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetActiveElectionsHandler> _logger;

    public GetActiveElectionsHandler(IElectionRepository electionRepository, ILogger<GetActiveElectionsHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Elections>> Handle(GetActiveElectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _electionRepository.GetActiveElectionsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching active elections: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}