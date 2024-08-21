using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetRecentFeedbacksQuery : IRequest<List<Feedback>>
{
    public DateTime FromDate { get; }

    public GetRecentFeedbacksQuery(DateTime Date)
    {
        FromDate = Date;
    }
}

