using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetFeedbackByIdHandler : IRequestHandler<GetFeedbackByIdQuery, Result<Feedback>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetFeedbackByIdHandler> _logger;

    public GetFeedbackByIdHandler(IFeedbackRepository feedbackRepository, ILogger<GetFeedbackByIdHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<Result<Feedback>> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = await _feedbackRepository.GetByIdAsync(request.FeedbackId);
            if (feedback == null)
            {
                return new Result<Feedback>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
            }
            return Ok(feedback);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the feedback with ID {FeedbackId}: {ErrorMessage}", request.FeedbackId, ex.Message);
            return new Result<Feedback>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
        }
    }
}