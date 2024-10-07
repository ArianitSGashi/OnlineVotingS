using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Infrastructure.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Feedback>> GetRecentFeedbacksAsync(DateTime date)
    {
        return await _dbSet.Where(f => f.FeedbackDate >= date).ToListAsync();
    }
}