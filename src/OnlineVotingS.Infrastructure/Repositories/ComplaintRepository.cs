using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Infrastructure.Repositories;

public class ComplaintRepository : GenericRepository<Complaints>, IComplaintRepository
{
    public ComplaintRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Complaints>> GetByUserIdAsync(string userId)
    {
        return await _dbSet.Where(c => c.UserID == userId).ToListAsync();
    }

    public async Task<IEnumerable<Complaints>> GetByElectionIdAsync(int electionId)
    {
        return await _dbSet.Where(c => c.ElectionID == electionId).ToListAsync();
    }

    public async Task<IEnumerable<Complaints>> GetByComplaintDateAsync(DateTime date)
    {
        return await _dbSet.Where(c => c.ComplaintDate.Date == date.Date).ToListAsync();
    }
}
