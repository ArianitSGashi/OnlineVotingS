using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class UpdateVoteCommand : IRequest<Votes>
{
    public VotesPutDTO VoteDto { get; }

    public UpdateVoteCommand(VotesPutDTO voteDto)
    {
        VoteDto = voteDto;
    }
}
