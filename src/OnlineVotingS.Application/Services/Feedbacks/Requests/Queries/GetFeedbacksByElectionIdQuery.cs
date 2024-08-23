using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbacksByElectionIdQuery : IRequest<IEnumerable<Feedback>>
{
    public int ElectionId { get; }

    public GetFeedbacksByElectionIdQuery(int electionId)
    {
        ElectionId = electionId;
    }
}