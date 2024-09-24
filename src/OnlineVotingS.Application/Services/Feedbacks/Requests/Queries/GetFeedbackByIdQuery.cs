using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbackByIdQuery : IRequest<Result<Feedback>>
{
    public int FeedbackId { get; }

    public GetFeedbackByIdQuery(int feedbackId)
    {
        FeedbackId = feedbackId;
    }
}