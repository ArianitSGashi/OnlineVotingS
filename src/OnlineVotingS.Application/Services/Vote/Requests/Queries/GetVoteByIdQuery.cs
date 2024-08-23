using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVoteByIdQuery : IRequest<Votes>
{
     public int VoteId { get; }

     public GetVoteByIdQuery(int voteId)
     {
         VoteId = voteId;
     }
}