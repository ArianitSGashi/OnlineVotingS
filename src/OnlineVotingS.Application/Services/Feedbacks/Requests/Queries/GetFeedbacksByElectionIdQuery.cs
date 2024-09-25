using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbacksByElectionIdQuery : IRequest<Result<IEnumerable<Feedback>>>
{
    public int ElectionId { get; }

    public GetFeedbacksByElectionIdQuery(int electionId)
    {
        ElectionId = electionId;
    }
}