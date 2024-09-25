using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class UpdateVoteCommandHandler : IRequestHandler<UpdateVoteCommand, Result<Votes>>
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

    public async Task<Result<Votes>> Handle(UpdateVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var vote = await _votesRepository.GetByIdAsync(request.VoteDto.VoteID);
            if (vote == null)
            {
                return new Result<Votes>().WithError(ErrorCodes.VOTE_NOT_FOUND.ToString());
            }

            _mapper.Map(request.VoteDto, vote);
            await _votesRepository.UpdateAsync(vote);
            return Ok(vote);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the vote with ID {VoteId}: {ErrorMessage}", request.VoteDto.VoteID, ex.Message);
            return new Result<Votes>().WithError(ErrorCodes.VOTE_UPDATE_FAILED.ToString());
        }
    }
}
