using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetCompletableElectionsQueryHandler : IRequestHandler<GetCompletableElectionsQuery, IEnumerable<Elections>>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetCompletableElectionsQueryHandler> _logger;

    public GetCompletableElectionsQueryHandler(IElectionRepository electionRepository, ILogger<GetCompletableElectionsQueryHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Elections>> Handle(GetCompletableElectionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var completableElections = await _electionRepository.GetCompletableElectionsAsync();
            return completableElections;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching completable elections");
            throw;
        }
    }
}