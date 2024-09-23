using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Feedbacks.Handlers.Commands;

public class CreateFeedbackHandler : IRequestHandler<CreateFeedbackCommand, Result<Feedback>>
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

    public async Task<Result<Feedback>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = _mapper.Map<Feedback>(request.FeedbackDto);
            await _feedbackRepository.AddAsync(feedback);

            _logger.LogInformation("Feedback created successfully with ID: {FeedbackId}", feedback);
            return Ok(feedback);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating feedback: {ErrorMessage}", ex.Message);
            return new Result<Feedback>().WithError(ErrorCodes.FEEDBACK_CREATION_FAILED.ToString());
        }
    }
}
