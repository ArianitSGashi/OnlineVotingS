using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;

public class CreateFeedbackCommand : IRequest<Feedback>
{
    public FeedbackPostDTO FeedbackDto { get; }

    public CreateFeedbackCommand(FeedbackPostDTO feedbackDto)
    {
        FeedbackDto = feedbackDto;
    }
}
