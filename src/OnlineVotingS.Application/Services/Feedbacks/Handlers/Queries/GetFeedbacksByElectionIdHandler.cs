using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Queries;

public class GetFeedbacksByElectionIdHandler : IRequestHandler<GetFeedbacksByElectionIdQuery, IEnumerable<Feedback>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILogger<GetFeedbacksByElectionIdHandler> _logger;

    public GetFeedbacksByElectionIdHandler(IFeedbackRepository feedbackRepository, ILogger<GetFeedbacksByElectionIdHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Feedback>> Handle(GetFeedbacksByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _feedbackRepository.GetByElectionIDAsync(request.ElectionId);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching feedbacks for election ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            throw;
        }
    }
}