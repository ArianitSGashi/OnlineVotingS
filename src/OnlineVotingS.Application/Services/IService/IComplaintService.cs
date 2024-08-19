﻿using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.IService;

public interface IComplaintService
{
    Task<Complaints> CreateComplaintsAsync(ComplaintsPostDTO complaintDto);
    Task<Complaints> UpdateComplaintsAsync(ComplaintsPutDTO complaintDto);
    Task<bool> DeleteComplaintsAsync(int complaintId);
    Task<Complaints> GetComplaintsByIdAsync(int complaintId);
    Task<IEnumerable<Complaints>> GetAllComplaintsAsync();
    Task<IEnumerable<Complaints>> GetComplaintByElectionIdAsync(int electionId);
    Task<IEnumerable<Complaints>> GetComplaintsByUserIdAsync(string userId);
}
