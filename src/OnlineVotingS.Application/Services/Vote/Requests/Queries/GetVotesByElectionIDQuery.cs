using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByElectionIDQuery : IRequest<Result<IEnumerable<Votes>>>
{
     public int ElectionID { get; }

     public GetVotesByElectionIDQuery(int electionID)
     {
        ElectionID = electionID;
     }
}