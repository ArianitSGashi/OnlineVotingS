using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Persistence.Repositories
{
    public class CandidateRepository : GenericRepository<Candidates>, ICandidateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CandidateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Candidates>> GetByElectionIdAsync(int electionId)
        {
            return await _dbContext.Candidates
                                   .Where(c => c.ElectionID == electionId)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Candidates>> GetByPartyAsync(string party)
        {
            return await _dbContext.Candidates
                                   .Where(c => c.Party == party)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Candidates>> GetByMinIncomeAsync(decimal minIncome)
        {
            return await _dbContext.Candidates
                                   .Where(c => c.Income >= minIncome)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Candidates>> GetByNameAsync(string name)
        {
            return await _dbContext.Candidates
                                   .Where(c => c.FullName.Contains(name))
                                   .ToListAsync();
        }
    }
}
