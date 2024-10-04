using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces;

public interface IRepliedComplaintsRepository : IGenericRepository<RepliedComplaints>
{
    Task<IEnumerable<RepliedComplaints>> GetByComplaintIDAsync(int complaintID);

    Task<IEnumerable<RepliedComplaints>> GetByReplyTextAsync(string replyText);

    Task<IEnumerable<RepliedComplaints>> GetRecentRepliesAsync(DateTime date);

    Task<int> GetTotalRepliedComplaintsCountAsync(); 

    Task<IEnumerable<RepliedComplaints>> GetRepliedComplaintsPaginatedAsync(int pageNumber, int pageSize); 

}