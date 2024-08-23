using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByElectionIDQuery : IRequest<IEnumerable<Votes>>
{
     public int ElectionID { get; }

     public GetVotesByElectionIDQuery(int electionID)
     {
        ElectionID = electionID;
     }
}