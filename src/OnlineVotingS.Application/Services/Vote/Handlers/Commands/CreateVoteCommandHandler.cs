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

public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, Result<Votes>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateVoteCommandHandler> _logger;

    public CreateVoteCommandHandler(
        IVotesRepository votesRepository,
        ICandidateRepository candidateRepository,
        IMapper mapper,
        ILogger<CreateVoteCommandHandler> logger)
    {
        _votesRepository = votesRepository;
        _candidateRepository = candidateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<Votes>> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _votesRepository.HasUserVotedInElectionAsync(request.VoteDto.UserID, request.VoteDto.ElectionID))
            {
                return new Result<Votes>().WithError(ErrorCodes.VOTE_ALREADY_CASTED_IN_THIS_ELECTION.ToString());
            }

            var candidateBelongsToElection = await _candidateRepository.CandidateBelongsToElectionAsync(request.VoteDto.CandidateID, request.VoteDto.ElectionID);
            if (!candidateBelongsToElection)
            {
                return new Result<Votes>().WithError(ErrorCodes.VOTE_NOT_FOUND.ToString());
            }

            var vote = _mapper.Map<Votes>(request.VoteDto);
            await _votesRepository.AddAsync(vote);
            return Ok(vote);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a vote for user {UserId}: {ErrorMessage}", request.VoteDto.UserID, ex.Message);
            return new Result<Votes>().WithError(ErrorCodes.VOTE_CREATION_FAILED.ToString());
        }
    }
}
