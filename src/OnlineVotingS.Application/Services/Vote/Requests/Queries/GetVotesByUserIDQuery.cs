using MediatR;
using FluentResults;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByUserIDQuery : IRequest<Result<bool>>
{
    public string UserID { get; }
    public int ElectionID { get; }

    public GetVotesByUserIDQuery(string userId, int electionId)
    {
        UserID = userId;
        ElectionID = electionId;
    }
}