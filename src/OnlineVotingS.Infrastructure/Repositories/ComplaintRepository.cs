using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Persistence.Repositories
{
    public class ComplaintRepository : GenericRepository<Complaints>, IComplaintRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ComplaintRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Complaints>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Complaints
                                   .Where(c => c.UserID == userId)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Complaints>> GetByElectionIdAsync(int electionId)
        {
            return await _dbContext.Complaints
                                   .Where(c => c.ElectionID == electionId)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Complaints>> GetByComplaintDateAsync(DateTime date)
        {
            return await _dbContext.Complaints
                                   .Where(c => c.ComplaintDate.Date == date.Date)
                                   .ToListAsync();
        }
    }
}
