using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetCompletableElectionsQueryHandler : IRequestHandler<GetCompletableElectionsQuery, Result<IEnumerable<Elections>>>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetCompletableElectionsQueryHandler> _logger;

    public GetCompletableElectionsQueryHandler(IElectionRepository electionRepository, ILogger<GetCompletableElectionsQueryHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Elections>>> Handle(GetCompletableElectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var completableElections = await _electionRepository.GetCompletableElectionsAsync();
            return Ok(completableElections);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching completable elections");
            return new Result<IEnumerable<Elections>>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
        }
    }
}