using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.ImplService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FeedbackService> _logger;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper, ILogger<FeedbackService> logger)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Feedback> CreateFeedbackAsync(FeedbackPostDTO feedbackDto)
        {
            try
            {
                var feedback = _mapper.Map<Feedback>(feedbackDto);
                await _feedbackRepository.AddAsync(feedback);
                _logger.LogInformation("Feedback created successfully with ID {FeedbackId}.", feedback.FeedbackID);
                return feedback;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating feedback: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        public async Task<Feedback> UpdateFeedbackAsync(FeedbackPutDTO feedbackDto)
        {
            try
            {
                var feedback = await _feedbackRepository.GetByIdAsync(feedbackDto.FeedbackID);
                if (feedback == null)
                {
                    _logger.LogWarning("Feedback with ID {FeedbackId} not found.", feedbackDto.FeedbackID);
                    throw new KeyNotFoundException($"Feedback with ID {feedbackDto.FeedbackID} not found.");
                }

                _mapper.Map(feedbackDto, feedback);
                await _feedbackRepository.UpdateAsync(feedback);
                _logger.LogInformation("Feedback with ID {FeedbackId} updated successfully.", feedbackDto.FeedbackID);
                return feedback;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating feedback with ID {FeedbackId}: {ErrorMessage}", feedbackDto.FeedbackID, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteFeedbackAsync(int feedbackId)
        {
            try
            {
                var exists = await _feedbackRepository.ExistsAsync(feedbackId);
                if (!exists)
                {
                    _logger.LogWarning("Feedback with ID {FeedbackId} not found.", feedbackId);
                    throw new KeyNotFoundException($"Feedback with ID {feedbackId} not found.");
                }

                await _feedbackRepository.DeleteAsync(feedbackId);
                _logger.LogInformation("Feedback with ID {FeedbackId} deleted successfully.", feedbackId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting feedback with ID {FeedbackId}: {ErrorMessage}", feedbackId, ex.Message);
                throw;
            }
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int feedbackId)
        {
            try
            {
                var feedback = await _feedbackRepository.GetByIdAsync(feedbackId);
                if (feedback == null)
                {
                    _logger.LogWarning("Feedback with ID {FeedbackId} not found.", feedbackId);
                    throw new KeyNotFoundException($"Feedback with ID {feedbackId} not found.");
                }

                return feedback;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching feedback with ID {FeedbackId}: {ErrorMessage}", feedbackId, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetAllAsync();
                return feedbacks;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching all feedbacks: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserIDAsync(string voterId)
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetByUserIDAsync(voterId);
                return feedbacks;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching feedbacks for user ID {VoterId}: {ErrorMessage}", voterId, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Feedback>> GetByElectionIDAsync(int electionId)
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetByElectionIDAsync(electionId);
                return feedbacks;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching feedbacks for election ID {ElectionId}: {ErrorMessage}", electionId, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Feedback>> GetRecentFeedbacksAsync(DateTime date)
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetRecentFeedbacksAsync(date);
                return feedbacks;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching recent feedbacks: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
