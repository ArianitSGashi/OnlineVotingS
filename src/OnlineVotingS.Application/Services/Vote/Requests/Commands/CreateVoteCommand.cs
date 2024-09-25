using MediatR;
using FluentResults;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class CreateVoteCommand : IRequest<Result<Votes>>
{
    public VotesPostDTO VoteDto { get; }

    public CreateVoteCommand(VotesPostDTO voteDto)
    {
        VoteDto = voteDto;
    }
}
