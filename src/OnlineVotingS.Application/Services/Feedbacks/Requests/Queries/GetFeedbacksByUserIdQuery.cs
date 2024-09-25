using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbacksByUserIdQuery : IRequest<Result<IEnumerable<Feedback>>>
{
    public string UserId { get; }

    public GetFeedbacksByUserIdQuery(string userId)
    {
        UserId = userId;
    }
}