using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetRecentFeedbacksQuery : IRequest<Result<IEnumerable<Feedback>>>
{
    public DateTime Date { get; }

    public GetRecentFeedbacksQuery(DateTime date)
    {
        Date = date;
    }
}