using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetRecentFeedbacksQuery : IRequest<IEnumerable<Feedback>>
{
    public DateTime Date { get; }

    public GetRecentFeedbacksQuery(DateTime date)
    {
        Date = date;
    }
}