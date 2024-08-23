using MediatR;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class DeleteVoteCommand : IRequest<bool>
{
    public int VoteId { get; }

    public DeleteVoteCommand(int voteId)
    {
        VoteId = voteId;
    }
}