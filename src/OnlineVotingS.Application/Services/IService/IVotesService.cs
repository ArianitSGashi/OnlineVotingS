using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.IService
{
    public interface IVotesService
    {
        Task<Votes> CreateVoteAsync(VotesPostDTO voteDto);
        Task<Votes> UpdateVoteAsync(VotesPutDTO voteDto);
        Task<bool> DeleteVoteAsync(int voteId);
        Task<Votes> GetVoteByIdAsync(int voteId);
        Task<IEnumerable<Votes>> GetAllVotesAsync();
        Task<IEnumerable<Votes>> GetVotesByUserIDAsync(string userID);
        Task<IEnumerable<Votes>> GetVotesByElectionIDAsync(int electionID);
        Task<IEnumerable<Votes>> GetVotesByCandidateIDAsync(int candidateID);
        Task<IEnumerable<Votes>> GetRecentVotesAsync(DateTime date);
    }
}