using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Commands;

public class CreateFeedbackHandler : IRequestHandler<CreateFeedbackCommand, Feedback>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateFeedbackHandler> _logger;

    public CreateFeedbackHandler(IFeedbackRepository feedbackRepository, IMapper mapper, ILogger<CreateFeedbackHandler> logger)
    {
        _feedbackRepository = feedbackRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Feedback> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = _mapper.Map<Feedback>(request.FeedbackDto);
            await _feedbackRepository.AddAsync(feedback);

            _logger.LogInformation("Feedback created successfully with ID {FeedbackId}.", feedback.FeedbackID);
            return feedback;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating feedback: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}

