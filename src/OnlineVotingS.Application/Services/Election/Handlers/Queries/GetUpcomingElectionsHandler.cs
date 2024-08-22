using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetUpcomingElectionsHandler : IRequestHandler<GetUpcomingElectionsQuery, IEnumerable<Elections>>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetUpcomingElectionsHandler> _logger;

    public GetUpcomingElectionsHandler(IElectionRepository electionRepository, ILogger<GetUpcomingElectionsHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Elections>> Handle(GetUpcomingElectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _electionRepository.GetUpcomingElectionsAsync(request.StartDate);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching upcoming elections: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}