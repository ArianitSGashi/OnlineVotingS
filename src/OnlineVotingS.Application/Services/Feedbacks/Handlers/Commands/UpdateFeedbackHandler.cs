using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Commands;

public class UpdateFeedbackHandler : IRequestHandler<UpdateFeedbackCommand, Result<Feedback>>
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

    public async Task<Result<Feedback>> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = await _feedbackRepository.GetByIdAsync(request.FeedbackDto.FeedbackID);
            if (feedback == null)
            {
                var errorMessage = $"Feedback with ID {request.FeedbackDto.FeedbackID} not found.";
                _logger.LogWarning(errorMessage);
                return new Result<Feedback>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
            }

            _mapper.Map(request.FeedbackDto, feedback);
            await _feedbackRepository.UpdateAsync(feedback);
            return Ok(feedback);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the feedback with ID {FeedbackId}: {ErrorMessage}", request.FeedbackDto.FeedbackID, ex.Message);
            return new Result<Feedback>().WithError(ErrorCodes.FEEDBACK_UPDATE_FAILED.ToString());
        }
    }
}
