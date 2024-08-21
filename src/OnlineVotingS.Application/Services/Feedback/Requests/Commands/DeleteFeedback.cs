using MediatR;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;

public class DeleteFeedbackCommand : IRequest<bool>
{
    public int FeedbackId { get; }

    public DeleteFeedbackCommand(int feedbackId)
    {
        FeedbackId = feedbackId;
    }
}

