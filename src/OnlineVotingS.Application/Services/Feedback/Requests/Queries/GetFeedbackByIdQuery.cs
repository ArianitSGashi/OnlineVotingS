using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbackByIdQuery : IRequest<Feedback>
{
    public int FeedbackId { get; }

    public GetFeedbackByIdQuery(int feedbackId)
    {
        FeedbackId = feedbackId;
    }
}
