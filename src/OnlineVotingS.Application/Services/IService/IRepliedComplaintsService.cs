using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.IService
{
    public interface IRepliedComplaintsService
    {
        Task<RepliedComplaints> CreateRepliedComplaintAsync(RepliedComplaintsPostDTO repliedComplaintsDto);
        Task<RepliedComplaints> UpdateRepliedComplaintAsync(RepliedComplaintsPutDTO repliedComplaintsDto);
        Task<bool> DeleteRepliedComplaintAsync(int repliedComplaintId);
        Task<RepliedComplaints> GetRepliedComplaintByIdAsync(int repliedComplaintId);
        Task<IEnumerable<RepliedComplaints>> GetAllRepliedComplaintsAsync();
    }
}