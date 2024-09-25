using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class UpdateVoteCommand : IRequest<Result<Votes>>
{
    public VotesPutDTO VoteDto { get; }

    public UpdateVoteCommand(VotesPutDTO voteDto)
    {
        VoteDto = voteDto;
    }
}
