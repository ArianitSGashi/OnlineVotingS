using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Commands;

public class UpdateFeedbackHandler : IRequestHandler<UpdateFeedbackCommand, FluentResults.Result<Feedback>>
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

    public async Task<FluentResults.Result<Feedback>> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = await _feedbackRepository.GetByIdAsync(request.FeedbackDto.FeedbackID);
            if (feedback == null)
            {
                var errorMessage = $"Feedback with ID {request.FeedbackDto.FeedbackID} not found.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage); 
            }

            _mapper.Map(request.FeedbackDto, feedback);
            await _feedbackRepository.UpdateAsync(feedback);
            _logger.LogInformation("Feedback with ID {FeedbackId} updated successfully.", feedback.FeedbackID);
            return FluentResults.Result.Ok(feedback); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the feedback with ID {FeedbackId}: {ErrorMessage}", request.FeedbackDto.FeedbackID, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex)); 
        }
    }
}
