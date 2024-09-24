using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetRecentFeedbacksHandler : IRequestHandler<GetRecentFeedbacksQuery, Result<IEnumerable<Feedback>>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetRecentFeedbacksHandler> _logger;

    public GetRecentFeedbacksHandler(IFeedbackRepository feedbackRepository, ILogger<GetRecentFeedbacksHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Feedback>>> Handle(GetRecentFeedbacksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var recentFeedbacks = await _feedbackRepository.GetRecentFeedbacksAsync(request.Date);
            return Ok(recentFeedbacks);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching recent feedbacks: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<Feedback>>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
        }
    }
}