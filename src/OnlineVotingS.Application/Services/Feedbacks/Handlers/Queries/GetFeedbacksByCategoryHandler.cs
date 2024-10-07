using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetFeedbacksByCategoryHandler : IRequestHandler<GetFeedbacksByCategoryQuery, Result<IEnumerable<Feedback>>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetFeedbacksByCategoryHandler> _logger;

    public GetFeedbacksByCategoryHandler(IFeedbackRepository feedbackRepository, ILogger<GetFeedbacksByCategoryHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Feedback>>> Handle(GetFeedbacksByCategoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackRepository.GetByCategoryAsync(request.Category);

            if (!feedbacks.Any())
            {
                return new Result<IEnumerable<Feedback>>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
            }

            return Ok(feedbacks);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching feedbacks for category {Category}: {ErrorMessage}", request.Category, ex.Message);
            return new Result<IEnumerable<Feedback>>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
        }
    }
}