using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByUserIDQuery : IRequest<IEnumerable<Votes>>
{
     public string UserID { get; }

     public GetVotesByUserIDQuery(string userID)
     {
         UserID = userID;
     }
}
