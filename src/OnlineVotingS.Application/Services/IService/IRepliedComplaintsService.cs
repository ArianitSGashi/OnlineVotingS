using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.IService;

public interface IRepliedComplaintsService
{
    Task<RepliedComplaints> CreateRepliedComplaintAsync(RepliedComplaintsPostDTO repliedComplaintDto);
    Task<RepliedComplaints> UpdateRepliedComplaintAsync(RepliedComplaintsPutDTO repliedComplaintDto);
    Task<bool> DeleteRepliedComplaintAsync(int repliedComplaintId);
    Task<RepliedComplaints> GetRepliedComplaintByIdAsync(int repliedComplaintId);
    Task<IEnumerable<RepliedComplaints>> GetAllRepliedComplaintsAsync();
    Task<IEnumerable<RepliedComplaints>> GetRepliedComplaintsByComplaintIDAsync(int complaintID);
    Task<IEnumerable<RepliedComplaints>> GetRepliedComplaintsByReplyTextAsync(string replyText);
    Task<IEnumerable<RepliedComplaints>> GetRecentRepliedComplaintsAsync(DateTime date);
}