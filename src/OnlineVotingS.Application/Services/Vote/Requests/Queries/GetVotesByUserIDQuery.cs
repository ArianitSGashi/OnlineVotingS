using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByUserIDQuery : IRequest<bool>
{
    public string UserID { get; }
    public int ElectionID { get; }

    public GetVotesByUserIDQuery(string userId, int electionId)
    {
        UserID = userId;
        ElectionID = electionId;
    }
}