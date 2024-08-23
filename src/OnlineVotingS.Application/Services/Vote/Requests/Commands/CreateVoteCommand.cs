using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class CreateVoteCommand : IRequest<Votes>
{
    public VotesPostDTO VoteDto { get; }

    public CreateVoteCommand(VotesPostDTO voteDto)
    {
        VoteDto = voteDto;
    }
}
