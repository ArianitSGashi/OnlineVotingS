using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetFeedbackByIdHandler : IRequestHandler<GetFeedbackByIdQuery, Feedback>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetFeedbackByIdHandler> _logger;

    public GetFeedbackByIdHandler(IFeedbackRepository feedbackRepository, ILogger<GetFeedbackByIdHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<Feedback> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = await _feedbackRepository.GetByIdAsync(request.FeedbackID);
            if (feedback == null)
            {
                _logger.LogWarning("Feedback with ID {FeedbackId} not found.", request.FeedbackID);
                throw new KeyNotFoundException($"Feedback with ID {request.FeedbackID} not found.");
            }

            return feedback;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the feedback with ID {FeedbackId}: {ErrorMessage}", request.FeedbackID, ex.Message);
            throw;
        }
    }
}

