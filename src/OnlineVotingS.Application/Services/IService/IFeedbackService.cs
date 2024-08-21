using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.IService;

public interface IFeedbackService
{
    Task<Feedback> CreateFeedbackAsync(FeedbackPostDTO feedbackDto);
    Task<Feedback> UpdateFeedbackAsync(FeedbackPutDTO feedbackDto);
    Task<bool> DeleteFeedbackAsync(int feedbackId);
    Task<Feedback> GetFeedbackByIdAsync(int feedbackId);
    Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();
    Task<IEnumerable<Feedback>> GetFeedbacksByUserIDAsync(string voterId);
    Task<IEnumerable<Feedback>> GetFeedbacksByElectionIDAsync(int electionId);
    Task<IEnumerable<Feedback>> GetRecentFeedbacksAsync(DateTime date);  
}
