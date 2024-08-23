using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByUserIDQuery : IRequest<IEnumerable<Votes>>
{
     public string UserID { get; }

     public GetVotesByUserIDQuery(string userID)
     {
         UserID = userID;
     }
}