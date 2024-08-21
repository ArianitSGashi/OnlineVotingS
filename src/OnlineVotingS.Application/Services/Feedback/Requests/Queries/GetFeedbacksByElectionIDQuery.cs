using MediatR;
using OnlineVotingS.Domain.Entities;
using System.Collections.Generic;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetFeedbackByElectionIdQuery : IRequest<List<Feedback>>
{
    public int ElectionId { get; }

    public GetFeedbackByElectionIdQuery(int electionId)
    {
        ElectionId = electionId;
    }
}
