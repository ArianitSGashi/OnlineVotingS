using FluentResults;
using MediatR;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class DeleteVoteCommand : IRequest<Result<bool>>
{
    public int VoteId { get; }

    public DeleteVoteCommand(int voteId)
    {
        VoteId = voteId;
    }
}
