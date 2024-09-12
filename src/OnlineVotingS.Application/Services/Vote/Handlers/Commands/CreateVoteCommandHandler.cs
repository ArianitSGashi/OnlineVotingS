using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.CostumExceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, Votes>
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

    public async Task<Votes> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if the user has already voted in this election
            if (await _votesRepository.HasUserVotedInElectionAsync(request.VoteDto.UserID, request.VoteDto.ElectionID))
            {
                throw new InvalidVoteException("You have already cast your vote in this election.");
            }
            // Check if the candidate belongs to the specified election
            var candidateBelongsToElection = await _candidateRepository.CandidateBelongsToElectionAsync(request.VoteDto.CandidateID, request.VoteDto.ElectionID);

            if (!candidateBelongsToElection)
            {
                throw new InvalidVoteException($"Candidate {request.VoteDto.CandidateID} does not belong to election {request.VoteDto.ElectionID}");
            }

            var vote = _mapper.Map<Votes>(request.VoteDto);
            await _votesRepository.AddAsync(vote);
            return vote;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {

            throw new InvalidVoteException("You have already cast your vote in this election.");
        }
        catch (InvalidVoteException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a vote: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}