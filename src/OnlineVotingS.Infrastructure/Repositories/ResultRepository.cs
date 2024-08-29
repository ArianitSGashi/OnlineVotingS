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

        // Method to get all results with related Elections and Candidates entities
        public async Task<IEnumerable<Result>> GetByElectionIdAsync(int electionId)
        {
            return await _dbSet.Where(r => r.ElectionID == electionId).ToListAsync();
        }

        // Corrected method to get a specific result by ID with related Elections and Candidates entities
        public async Task<Result> GetResultWithDetailsAsync(int resultId)
        {
            return await _dbSet.Include(r => r.Elections)  // Use 'Elections' to match the entity property
                               .Include(r => r.Candidates) // Use 'Candidates' to match the entity property
                               .FirstOrDefaultAsync(r => r.ResultID == resultId);
        }

        public async Task<IEnumerable<Result>> GetByCandidateIdAsync(int candidateId)
        {
            return await _dbSet.Where(r => r.CandidateID == candidateId).ToListAsync();
        }

        // Method to get results with total votes greater than a specified value with related entities
        public async Task<IEnumerable<Result>> GetByTotalVotesGreaterThanAsync(int votes)
        {
            return await _dbSet.Where(r => r.TotalVotes > votes).ToListAsync();
        }

        // Method to get a specific result by ID (uses GetResultWithDetailsAsync internally)
        public async Task<Result> GetByIdAsync(int id)
        {
            return await GetResultWithDetailsAsync(id);
        }
        public async Task<Result> GetByResultIdAsync(int resultId)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.ResultID == resultId);
        }
    }
}
