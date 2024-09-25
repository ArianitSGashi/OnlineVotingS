using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetFeedbacksByElectionIdHandler : IRequestHandler<GetFeedbacksByElectionIdQuery, Result<IEnumerable<Feedback>>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetFeedbacksByElectionIdHandler> _logger;

    public GetFeedbacksByElectionIdHandler(IFeedbackRepository feedbackRepository, ILogger<GetFeedbacksByElectionIdHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Feedback>>> Handle(GetFeedbacksByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackRepository.GetByElectionIDAsync(request.ElectionId);
            return Ok(feedbacks);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching feedbacks for election ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            return new Result<IEnumerable<Feedback>>().WithError(ErrorCodes.FEEDBACK_NOT_FOUND.ToString());
        }
    }
}