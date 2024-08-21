using MediatR;
using OnlineVotingS.Domain.Entities;
using System.Collections.Generic;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbackByUserIdQuery : IRequest<List<Feedback>>
{
    public int UserId { get; }

    public GetFeedbackByUserIdQuery(int userId)
    {
        UserId = userId;
    }
}

