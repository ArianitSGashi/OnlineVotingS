using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Commands;

public class DeleteFeedbackHandler : IRequestHandler<DeleteFeedbackCommand, Result<bool>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<DeleteFeedbackHandler> _logger;

    public DeleteFeedbackHandler(IFeedbackRepository feedbackRepository, ILogger<DeleteFeedbackHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _feedbackRepository.ExistsAsync(request.FeedbackId);
            if (!exists)
            {
                var errorMessage = $"Feedback with ID {request.FeedbackId} not found.";
                return new Result<bool>().WithError(errorMessage);
            }

            await _feedbackRepository.DeleteAsync(request.FeedbackId);
            return Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the feedback with ID {FeedbackId}: {ErrorMessage}", request.FeedbackId, ex.Message);
            return new Result<bool>().WithError(ErrorCodes.FEEDBACK_DELETION_FAILED.ToString());
        }
    }
}
