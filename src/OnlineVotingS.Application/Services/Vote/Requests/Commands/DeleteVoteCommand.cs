using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class DeleteVoteCommand : IRequest<bool>
{
    public int VoteId { get; }

    public DeleteVoteCommand(int voteId)
    {
        VoteId = voteId;
    }
}
