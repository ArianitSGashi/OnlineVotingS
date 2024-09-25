using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Errors;
using Microsoft.Extensions.Logging;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetActiveElectionsHandler : IRequestHandler<GetActiveElectionsQuery, Result<IEnumerable<Elections>>>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly ILogger<GetActiveElectionsHandler> _logger;

    public GetActiveElectionsHandler(IElectionRepository electionsRepository, ILogger<GetActiveElectionsHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Elections>>> Handle(GetActiveElectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allElections = await _electionsRepository.GetAllAsync();
            var now = DateTime.Now;
            var activeElections = allElections.Where(e =>
                (e.Status == ElectionStatus.Active) ||
                (e.Status != ElectionStatus.Completed &&
                 now >= e.StartDate.ToDateTime(TimeOnly.FromTimeSpan(e.StartTime)) &&
                 now <= e.EndDate.ToDateTime(TimeOnly.FromTimeSpan(e.EndTime)))
            );
            return Ok(activeElections);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching active elections");
            return new Result<IEnumerable<Elections>>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
        }
    }
}