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

public class GetFeedbacksByUserIdHandler : IRequestHandler<GetFeedbacksByUserIdQuery, IEnumerable<Feedback>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetFeedbacksByUserIdHandler> _logger;

    public GetFeedbacksByUserIdHandler(IFeedbackRepository feedbackRepository, ILogger<GetFeedbacksByUserIdHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Feedback>> Handle(GetFeedbacksByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackRepository.GetByUserIdAsync(request.UserID);
            return feedbacks;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching feedbacks for user ID {UserId}: {ErrorMessage}", request.UserID, ex.Message);
            throw;
        }
    }
}
