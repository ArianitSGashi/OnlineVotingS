using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class UpdateVoteCommandHandler : IRequestHandler<UpdateVoteCommand, Votes>
{
    private readonly IVotesRepository _votesRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateVoteCommandHandler> _logger;

    public UpdateVoteCommandHandler(IVotesRepository votesRepository, IMapper mapper, ILogger<UpdateVoteCommandHandler> logger)
    {
        _votesRepository = votesRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Votes> Handle(UpdateVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var vote = await _votesRepository.GetByIdAsync(request.VoteDto.VoteID);
            if (vote == null)
            {
                _logger.LogWarning("Vote with ID {VoteId} not found.", request.VoteDto.VoteID);
                throw new KeyNotFoundException($"Vote with ID {request.VoteDto.VoteID} not found.");
            }

            _mapper.Map(request.VoteDto, vote);
            await _votesRepository.UpdateAsync(vote);

            _logger.LogInformation("Vote with ID {VoteId} updated successfully.", request.VoteDto.VoteID);
            return vote;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the vote with ID {VoteId}: {ErrorMessage}", request.VoteDto.VoteID, ex.Message);
            throw;
        }
    }
}
