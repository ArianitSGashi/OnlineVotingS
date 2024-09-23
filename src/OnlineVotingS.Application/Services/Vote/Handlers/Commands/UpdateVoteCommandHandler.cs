using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class UpdateVoteCommandHandler : IRequestHandler<UpdateVoteCommand, FluentResults.Result<Votes>>
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

    public async Task<FluentResults.Result<Votes>> Handle(UpdateVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var vote = await _votesRepository.GetByIdAsync(request.VoteDto.VoteID);
            if (vote == null)
            {
                var errorMessage = $"Vote with ID {request.VoteDto.VoteID} not found.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage);
            }

            _mapper.Map(request.VoteDto, vote);
            await _votesRepository.UpdateAsync(vote);
            _logger.LogInformation("Vote with ID {VoteId} updated successfully.", request.VoteDto.VoteID);
            return FluentResults.Result.Ok(vote); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the vote with ID {VoteId}: {ErrorMessage}", request.VoteDto.VoteID, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
