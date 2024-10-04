using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Infrastructure.Repositories;

public class CandidateRepository : GenericRepository<Candidates>, ICandidateRepository
{
    public CandidateRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Candidates>> GetByElectionIdAsync(int electionId)
    {
        return await _dbSet.Where(c => c.ElectionID == electionId).ToListAsync();
    }

    public async Task<IEnumerable<Candidates>> GetByPartyAsync(string party)
    {
        return await _dbSet.Where(c => c.Party == party).ToListAsync();
    }

    public async Task<IEnumerable<Candidates>> GetByMinIncomeAsync(decimal minIncome)
    {
        return await _dbSet.Where(c => c.Income >= minIncome).ToListAsync();
    }

    public async Task<IEnumerable<Candidates>> GetByNameAsync(string name)
    {
        return await _dbSet.Where(c => c.FullName.Contains(name)).ToListAsync();
    }

    public async Task<bool> CandidateBelongsToElectionAsync(int candidateId, int electionId)
    {
        return await _dbSet.AnyAsync(c => c.CandidateID == candidateId && c.ElectionID == electionId);
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesPaginatedAsync(int pageNumber, int pageSize)
    {
        return await _context.Candidates
            .OrderBy(c => c.CandidateID) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCandidatesCountAsync()
    {
        return await _context.Candidates.CountAsync();
    }



}
