using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class DeleteVoteCommandHandler : IRequestHandler<DeleteVoteCommand, bool>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<DeleteVoteCommandHandler> _logger;

    public DeleteVoteCommandHandler(IVotesRepository votesRepository, ILogger<DeleteVoteCommandHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _votesRepository.ExistsAsync(request.VoteId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Vote with ID {request.VoteId} not found.");
            }

            await _votesRepository.DeleteAsync(request.VoteId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the vote with ID {VoteId}: {ErrorMessage}", request.VoteId, ex.Message);
            throw;
        }
    }
}
