using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetAllFeedbacksQuery : IRequest<IEnumerable<Feedback>>
{
}