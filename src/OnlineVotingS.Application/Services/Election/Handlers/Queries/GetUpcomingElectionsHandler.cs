using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetUpcomingElectionsHandler : IRequestHandler<GetUpcomingElectionsQuery, Result<IEnumerable<Elections>>>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetUpcomingElectionsHandler> _logger;

    public GetUpcomingElectionsHandler(IElectionRepository electionRepository, ILogger<GetUpcomingElectionsHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Elections>>> Handle(GetUpcomingElectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var upcomingElections = await _electionRepository.GetUpcomingElectionsAsync(request.StartDate);
            return Ok(upcomingElections);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching upcoming elections: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<Elections>>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
        }
    }
}