﻿using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class GetVotesByElectionIDQueryHandler : IRequestHandler<GetVotesByElectionIDQuery, Result<IEnumerable<Votes>>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<GetVotesByElectionIDQueryHandler> _logger;

    public GetVotesByElectionIDQueryHandler(IVotesRepository votesRepository, ILogger<GetVotesByElectionIDQueryHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Votes>>> Handle(GetVotesByElectionIDQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var votes = await _votesRepository.GetByElectionIDAsync(request.ElectionID);
            return Ok(votes);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching votes for election ID {ElectionID}: {ErrorMessage}", request.ElectionID, ex.Message);
            return new Result<IEnumerable<Votes>>().WithError(ErrorCodes.VOTE_NOT_FOUND.ToString());
        }
    }
}