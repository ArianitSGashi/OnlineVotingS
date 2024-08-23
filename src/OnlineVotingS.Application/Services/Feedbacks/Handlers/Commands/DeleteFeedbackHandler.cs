using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Commands;

public class DeleteFeedbackHandler : IRequestHandler<DeleteFeedbackCommand, bool>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<DeleteFeedbackHandler> _logger;

    public DeleteFeedbackHandler(IFeedbackRepository feedbackRepository, ILogger<DeleteFeedbackHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _feedbackRepository.ExistsAsync(request.FeedbackId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Feedback with ID {request.FeedbackId} not found.");
            }

            await _feedbackRepository.DeleteAsync(request.FeedbackId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the feedback with ID {FeedbackId}: {ErrorMessage}", request.FeedbackId, ex.Message);
            throw;
        }
    }
}