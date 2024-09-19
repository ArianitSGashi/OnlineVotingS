using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class GetVotesByUserIDQueryHandler : IRequestHandler<GetVotesByUserIDQuery, bool>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<GetVotesByUserIDQueryHandler> _logger;

    public GetVotesByUserIDQueryHandler(IVotesRepository votesRepository, ILogger<GetVotesByUserIDQueryHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(GetVotesByUserIDQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _votesRepository.HasUserVotedInElectionAsync(request.UserID, request.ElectionID);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while checking if user ID {UserID} has voted in election ID {ElectionID}: {ErrorMessage}", request.UserID, request.ElectionID, ex.Message);
            throw;
        }
    }
}