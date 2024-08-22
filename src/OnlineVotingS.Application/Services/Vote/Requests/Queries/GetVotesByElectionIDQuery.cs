using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByElectionIDQuery : IRequest<IEnumerable<Votes>>
{
     public int ElectionID { get; }

     public GetVotesByElectionIDQuery(int electionID)
     {
        ElectionID = electionID;
     }
}
