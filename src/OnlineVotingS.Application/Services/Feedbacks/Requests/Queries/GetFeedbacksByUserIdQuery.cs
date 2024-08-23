using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbacksByUserIdQuery : IRequest<IEnumerable<Feedback>>
{
    public string UserId { get; }

    public GetFeedbacksByUserIdQuery(string userId)
    {
        UserId = userId;
    }
}