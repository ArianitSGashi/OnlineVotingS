using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVoteByIdQuery : IRequest<Votes>
{
     public int VoteId { get; }

     public GetVoteByIdQuery(int voteId)
     {
         VoteId = voteId;
     }
}
