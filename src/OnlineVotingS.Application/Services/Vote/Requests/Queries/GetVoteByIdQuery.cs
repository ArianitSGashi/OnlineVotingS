using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVoteByIdQuery : IRequest<Result<Votes>>
{
     public int VoteId { get; }

     public GetVoteByIdQuery(int voteId)
     {
         VoteId = voteId;
     }
}