using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Infrastructure.Repositories;

public class VotesRepository : GenericRepository<Votes>, IVotesRepository
{
    public VotesRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Votes>> GetByUserIDAsync(string userID)
    {
        return await _dbSet.Where(v => v.UserID == userID).ToListAsync();
    }

    public async Task<IEnumerable<Votes>> GetByElectionIDAsync(int electionID)
    {
        return await _dbSet.Where(v => v.ElectionID == electionID).ToListAsync();
    }

    public async Task<IEnumerable<Votes>> GetByCandidateIDAsync(int candidateID)
    {
        return await _dbSet.Where(v => v.CandidateID == candidateID).ToListAsync();
    }

    public async Task<IEnumerable<Votes>> GetRecentVotesAsync(DateTime date)
    {
        return await _dbSet.Where(v => v.VoteDate >= date).ToListAsync();
    }

    public async Task<bool> HasUserVotedInElectionAsync(string userId, int electionId)
    {
        return await _dbSet.AnyAsync(v => v.UserID == userId && v.ElectionID == electionId);
    }
}