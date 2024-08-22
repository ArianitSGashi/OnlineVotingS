using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetAllElectionsHandler : IRequestHandler<GetAllElectionsQuery, IEnumerable<Elections>>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetAllElectionsHandler> _logger;

    public GetAllElectionsHandler(IElectionRepository electionRepository, ILogger<GetAllElectionsHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Elections>> Handle(GetAllElectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _electionRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all elections: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}