using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.ImplService
{
    public class VotesService : IVotesService
    {
        private readonly IVotesRepository _votesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VotesService> _logger;

        public VotesService(IVotesRepository votesRepository, IMapper mapper, ILogger<VotesService> logger)
        {
            _votesRepository = votesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Votes> CreateVoteAsync(VotesPostDTO voteDto)
        {
            try
            {
                var vote = _mapper.Map<Votes>(voteDto);
                await _votesRepository.AddAsync(vote);

                _logger.LogInformation("Vote created successfully with ID {VoteId}.", vote.VoteID);
                return vote;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating a vote: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        public async Task<Votes> UpdateVoteAsync(VotesPutDTO voteDto)
        {
            try
            {
                var vote = await _votesRepository.GetByIdAsync(voteDto.VoteID);
                if (vote == null)
                {
                    _logger.LogWarning("Vote with ID {VoteId} not found.", voteDto.VoteID);
                    throw new KeyNotFoundException($"Vote with ID {voteDto.VoteID} not found.");
                }

                _mapper.Map(voteDto, vote);
                await _votesRepository.UpdateAsync(vote);

                _logger.LogInformation("Vote with ID {VoteId} updated successfully.", voteDto.VoteID);
                return vote;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating the vote with ID {VoteId}: {ErrorMessage}", voteDto.VoteID, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteVoteAsync(int voteId)
        {
            try
            {
                var exists = await _votesRepository.ExistsAsync(voteId);
                if (!exists)
                {
                    _logger.LogWarning("Vote with ID {VoteId} not found.", voteId);
                    throw new KeyNotFoundException($"Vote with ID {voteId} not found.");
                }

                await _votesRepository.DeleteAsync(voteId);

                _logger.LogInformation("Vote with ID {VoteId} deleted successfully.", voteId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting the vote with ID {VoteId}: {ErrorMessage}", voteId, ex.Message);
                throw;
            }
        }

        public async Task<Votes> GetVoteByIdAsync(int voteId)
        {
            try
            {
                var vote = await _votesRepository.GetByIdAsync(voteId);
                if (vote == null)
                {
                    _logger.LogWarning("Vote with ID {VoteId} not found.", voteId);
                    throw new KeyNotFoundException($"Vote with ID {voteId} not found.");
                }

                return vote;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching the vote with ID {VoteId}: {ErrorMessage}", voteId, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Votes>> GetAllVotesAsync()
        {
            try
            {
                var votes = await _votesRepository.GetAllAsync();
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching all votes: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Votes>> GetVotesByUserIDAsync(string userID)
        {
            try
            {
                var votes = await _votesRepository.GetByUserIDAsync(userID);
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching votes for user ID {UserID}: {ErrorMessage}", userID, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Votes>> GetVotesByElectionIDAsync(int electionID)
        {
            try
            {
                var votes = await _votesRepository.GetByElectionIDAsync(electionID);
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching votes for election ID {ElectionID}: {ErrorMessage}", electionID, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Votes>> GetVotesByCandidateIDAsync(int candidateID)
        {
            try
            {
                var votes = await _votesRepository.GetByCandidateIDAsync(candidateID);
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching votes for candidate ID {CandidateID}: {ErrorMessage}", candidateID, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Votes>> GetRecentVotesAsync(DateTime date)
        {
            try
            {
                var votes = await _votesRepository.GetRecentVotesAsync(date);
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching recent votes: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}