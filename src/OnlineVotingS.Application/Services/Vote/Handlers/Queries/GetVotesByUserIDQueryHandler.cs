using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class GetVotesByUserIDQueryHandler : IRequestHandler<GetVotesByUserIDQuery, Result<bool>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<GetVotesByUserIDQueryHandler> _logger;

    public GetVotesByUserIDQueryHandler(IVotesRepository votesRepository, ILogger<GetVotesByUserIDQueryHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(GetVotesByUserIDQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var hasVoted = await _votesRepository.HasUserVotedInElectionAsync(request.UserID, request.ElectionID);
            return Ok(hasVoted);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while checking if user ID {UserID} has voted in election ID {ElectionID}: {ErrorMessage}", request.UserID, request.ElectionID, ex.Message);
            return new Result<bool>().WithError(ErrorCodes.VOTE_NOT_FOUND.ToString());
        }
    }
}