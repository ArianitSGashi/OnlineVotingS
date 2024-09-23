using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;

public class UpdateFeedbackCommand : IRequest<Result<Feedback>>
{
    public FeedbackPutDTO FeedbackDto { get; }

    public UpdateFeedbackCommand(FeedbackPutDTO feedbackDto)
    {
        FeedbackDto = feedbackDto;
    }
}
