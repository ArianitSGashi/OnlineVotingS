using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<RepliedComplaints>> GetByUserIdAsync(int userId)
        {
            return await _dbSet.Where(rc => rc.userId == userId).ToListAsync();
        }

        public async Task<List<RepliedComplaints>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet.Where(rc => rc.ReplyDate >= startDate && rc.ReplyDate <= endDate).ToListAsync();
        }
    }
}
