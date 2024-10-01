using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;

namespace OnlineVotingS.Infrastructure.Repositories
{
    public class RepliedComplaintsRepository : GenericRepository<RepliedComplaints>, IRepliedComplaintsRepository
    {
        public RepliedComplaintsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RepliedComplaints>> GetByComplaintIDAsync(int complaintID)
        {
            return await _dbSet.Where(rc => rc.ComplaintID == complaintID).ToListAsync();
        }

        public async Task<IEnumerable<RepliedComplaints>> GetByReplyTextAsync(string replyText)
        {
            return await _dbSet.Where(rc => rc.ReplyText.Contains(replyText)).ToListAsync();
        }

        public async Task<IEnumerable<RepliedComplaints>> GetRecentRepliesAsync(DateTime date)
        {
            return await _dbSet.Where(rc => rc.ReplyDate >= date).ToListAsync();
        }

        public async Task<int> GetTotalRepliedComplaintsCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<IEnumerable<RepliedComplaints>> GetRepliedComplaintsPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .OrderBy(rc => rc.RepliedComplaintID) // Ensure consistent ordering
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
