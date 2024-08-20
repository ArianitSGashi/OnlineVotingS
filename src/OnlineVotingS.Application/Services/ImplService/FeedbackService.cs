using AutoMapper;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.ImplService;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IMapper _mapper;

    public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper)
    {
        _feedbackRepository = feedbackRepository;
        _mapper = mapper;
    }

    public async Task<Feedback> CreateFeedbackAsync(FeedbackPostDTO feedbackDto)
    {
        var feedback = _mapper.Map<Feedback>(feedbackDto);
        await _feedbackRepository.AddAsync(feedback);
        return feedback;
    }

    public async Task<Feedback> UpdateFeedbackAsync(FeedbackPutDTO feedbackDto)
    {
        var feedback = await _feedbackRepository.GetByIdAsync(feedbackDto.FeedbackID);
        if (feedback == null)
        {
            throw new KeyNotFoundException($"Feedback with ID {feedbackDto.FeedbackID} not found.");
        }

        _mapper.Map(feedbackDto, feedback);
        await _feedbackRepository.UpdateAsync(feedback);
        return feedback;
    }

    public async Task<bool> DeleteFeedbackAsync(int feedbackId)
    {
        var exists = await _feedbackRepository.ExistsAsync(feedbackId);
        if (!exists)
        {
            throw new KeyNotFoundException($"Feedback with ID {feedbackId} not found.");
        }

        await _feedbackRepository.DeleteAsync(feedbackId);
        return true;
    }

    public async Task<Feedback> GetFeedbackByIdAsync(int feedbackId)
    {
        return await _feedbackRepository.GetByIdAsync(feedbackId);
    }

    public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
    {
        return await _feedbackRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Feedback>> GetFeedbacksByUserIDAsync(string voterId)
    {
        return await _feedbackRepository.GetByUserIDAsync(voterId);
    }

    public async Task<IEnumerable<Feedback>> GetByElectionIDAsync(int electionId)
    {
        return await _feedbackRepository.GetByElectionIDAsync(electionId);
    }

    public async Task<IEnumerable<Feedback>> GetRecentFeedbacksAsync(DateTime date)
    {
        return await _feedbackRepository.GetRecentFeedbacksAsync(date);
    }
}
