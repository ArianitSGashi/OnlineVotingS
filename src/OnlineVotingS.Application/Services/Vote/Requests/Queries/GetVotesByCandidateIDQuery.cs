using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByCandidateIDQuery : IRequest<IEnumerable<Votes>>
{
     public int CandidateID { get; }

     public GetVotesByCandidateIDQuery(int candidateID)
     {
          CandidateID = candidateID;
      }
}