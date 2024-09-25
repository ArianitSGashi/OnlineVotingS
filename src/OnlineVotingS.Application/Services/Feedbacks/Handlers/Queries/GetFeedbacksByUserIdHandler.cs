using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetFeedbacksByUserIdHandler : IRequestHandler<GetFeedbacksByUserIdQuery, Result<IEnumerable<Feedback>>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetFeedbacksByUserIdHandler> _logger;

    public GetFeedbacksByUserIdHandler(IFeedbackRepository feedbackRepository, ILogger<GetFeedbacksByUserIdHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Feedback>>> Handle(GetFeedbacksByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackRepository.GetByUserIDAsync(request.UserId);
            return Ok(feedbacks);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching feedbacks for user ID {UserId}: {ErrorMessage}", request.UserId, ex.Message);
            return new Result<IEnumerable<Feedback>>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
        }
    }
}