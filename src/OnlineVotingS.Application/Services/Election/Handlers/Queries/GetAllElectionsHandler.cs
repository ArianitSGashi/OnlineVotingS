using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetAllElectionsHandler : IRequestHandler<GetAllElectionsQuery, Result<IEnumerable<Elections>>>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetAllElectionsHandler> _logger;

    public GetAllElectionsHandler(IElectionRepository electionRepository, ILogger<GetAllElectionsHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Elections>>> Handle(GetAllElectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var elections = await _electionRepository.GetAllAsync();
            return Ok(elections);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all elections: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<Elections>>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
        }
    }
}