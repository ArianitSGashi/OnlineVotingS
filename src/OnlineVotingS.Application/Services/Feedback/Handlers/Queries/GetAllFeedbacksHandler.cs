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

public class GetAllFeedbacksHandler : IRequestHandler<GetAllFeedbacksQuery, IEnumerable<Feedback>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetAllFeedbacksHandler> _logger;

    public GetAllFeedbacksHandler(IFeedbackRepository feedbackRepository, ILogger<GetAllFeedbacksHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Feedback>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _feedbackRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all feedbacks: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
