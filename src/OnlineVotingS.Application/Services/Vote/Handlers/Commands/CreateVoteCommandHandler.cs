using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.CostumExceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, FluentResults.Result<Votes>>
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

    public async Task<FluentResults.Result<Votes>> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _votesRepository.HasUserVotedInElectionAsync(request.VoteDto.UserID, request.VoteDto.ElectionID))
            {
                var errorMessage = "You have already cast your vote in this election.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage);  
            }

            var candidateBelongsToElection = await _candidateRepository.CandidateBelongsToElectionAsync(request.VoteDto.CandidateID, request.VoteDto.ElectionID);
            if (!candidateBelongsToElection)
            {
                var errorMessage = $"Candidate {request.VoteDto.CandidateID} does not belong to election {request.VoteDto.ElectionID}.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage);  
            }

            var vote = _mapper.Map<Votes>(request.VoteDto);
            await _votesRepository.AddAsync(vote);

            _logger.LogInformation("Vote created successfully for user {UserId} in election {ElectionId}.", request.VoteDto.UserID, request.VoteDto.ElectionID);
            return FluentResults.Result.Ok(vote);  
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            var errorMessage = "You have already cast your vote in this election.";
            _logger.LogWarning(errorMessage);
            return FluentResults.Result.Fail(errorMessage);  
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a vote for user {UserId}: {ErrorMessage}", request.VoteDto.UserID, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex)); 
        }
    }
}
