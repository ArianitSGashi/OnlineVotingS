using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetAllFeedbacksHandler : IRequestHandler<GetAllFeedbacksQuery, Result<IEnumerable<Feedback>>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetAllFeedbacksHandler> _logger;

    public GetAllFeedbacksHandler(IFeedbackRepository feedbackRepository, ILogger<GetAllFeedbacksHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Feedback>>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            return Ok(feedbacks);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all feedbacks: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<Feedback>>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
        }
    }
}