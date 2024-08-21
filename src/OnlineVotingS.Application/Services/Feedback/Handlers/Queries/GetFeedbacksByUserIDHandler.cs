using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

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
            return await _feedbackRepository.GetByUserIDAsync(request.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching feedbacks for user ID {UserId}: {ErrorMessage}", request.UserId, ex.Message);
            throw;
        }
    }
}