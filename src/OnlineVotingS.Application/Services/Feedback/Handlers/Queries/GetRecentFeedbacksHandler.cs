using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetRecentFeedbacksHandler : IRequestHandler<GetRecentFeedbacksQuery, IEnumerable<Feedback>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetRecentFeedbacksHandler> _logger;

    public GetRecentFeedbacksHandler(IFeedbackRepository feedbackRepository, ILogger<GetRecentFeedbacksHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Feedback>> Handle(GetRecentFeedbacksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackRepository.GetRecentFeedbacksAsync(request.Date);
            return feedbacks;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching recent feedbacks: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}

