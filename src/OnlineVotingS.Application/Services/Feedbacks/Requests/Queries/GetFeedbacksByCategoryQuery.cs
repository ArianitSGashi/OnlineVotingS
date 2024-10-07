using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Enums;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbacksByCategoryQuery : IRequest<Result<IEnumerable<Feedback>>>
{
    public FeedbackCategory Category { get; }

    public GetFeedbacksByCategoryQuery(FeedbackCategory category)
    {
        Category = category;
    }
}