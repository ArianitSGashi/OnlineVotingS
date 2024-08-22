using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces;

    public interface IRepliedComplaintsRepository : IGenericRepository<RepliedComplaints>
    {
        Task<IEnumerable<RepliedComplaints>> GetByComplaintIDAsync(int complaintID);
    Task<List<RepliedComplaints>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<RepliedComplaints>> GetByReplyTextAsync(string replyText);
    Task<List<RepliedComplaints>> GetByUserIdAsync(int userId);
    Task<IEnumerable<RepliedComplaints>> GetRecentRepliesAsync(DateTime date);
    }
