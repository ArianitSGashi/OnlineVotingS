using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetAllFeedbacksQuery : IRequest<Result<IEnumerable<Feedback>>>
{
}