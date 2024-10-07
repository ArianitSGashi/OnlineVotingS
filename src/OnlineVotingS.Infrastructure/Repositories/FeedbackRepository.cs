using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Infrastructure.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Bazuar ne kategori
        public async Task<IEnumerable<Feedback>> GetByCategoryAsync(FeedbackCategory category)
        {
            return await _dbSet.Where(f => f.FeedbackCategory == category).ToListAsync();
        }

        // Bazuar ne data
        public async Task<IEnumerable<Feedback>> GetRecentFeedbacksAsync(DateTime date)
        {
            return await _dbSet.Where(f => f.FeedbackDate >= date).ToListAsync();
        }
    }
}