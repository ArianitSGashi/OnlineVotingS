using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Commands;

public class UpdateVoteCommand : IRequest<Votes>
{
    public VotesPutDTO VoteDto { get; }

    public UpdateVoteCommand(VotesPutDTO voteDto)
    {
        VoteDto = voteDto;
    }
}