using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Infrastructure.Repositories
{
    public class ResultRepository : GenericRepository<Result>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Result>> GetByElectionIdAsync(int electionId)
        {
            return await _dbSet.Where(r => r.ElectionID == electionId).ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetByCandidateIdAsync(int candidateId)
        {
            return await _dbSet.Where(r => r.CandidateID == candidateId).ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetByTotalVotesGreaterThanAsync(int votes)
        {
            return await _dbSet.Where(r => r.TotalVotes > votes).ToListAsync();
        }
    }
}