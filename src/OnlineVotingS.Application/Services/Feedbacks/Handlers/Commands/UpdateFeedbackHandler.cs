using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Commands;

public class UpdateFeedbackHandler : IRequestHandler<UpdateFeedbackCommand, Feedback>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateFeedbackHandler> _logger;

    public UpdateFeedbackHandler(IFeedbackRepository feedbackRepository, IMapper mapper, ILogger<UpdateFeedbackHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Feedback> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = await _feedbackRepository.GetByIdAsync(request.FeedbackDto.FeedbackID);
            if (feedback == null)
            {
                _logger.LogWarning("Feedback with ID {FeedbackId} not found.", request.FeedbackDto.FeedbackID);
                throw new KeyNotFoundException($"Feedback with ID {request.FeedbackDto.FeedbackID} not found.");
            }

            _mapper.Map(request.FeedbackDto, feedback);
            await _feedbackRepository.UpdateAsync(feedback);
            _logger.LogInformation("Feedback with ID {FeedbackId} updated successfully.", request.FeedbackDto.FeedbackID);
            return feedback;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the feedback with ID {FeedbackId}: {ErrorMessage}", request.FeedbackDto.FeedbackID, ex.Message);
            throw;
        }
    }
}