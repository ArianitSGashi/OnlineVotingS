using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class CreateVoteCommand : IRequest<Votes>
{
    public VotesPostDTO VoteDto { get; }

    public CreateVoteCommand(VotesPostDTO voteDto)
    {
        VoteDto = voteDto;
    }
}