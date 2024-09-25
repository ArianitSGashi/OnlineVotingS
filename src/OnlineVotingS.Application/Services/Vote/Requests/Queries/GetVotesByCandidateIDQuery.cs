using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByCandidateIDQuery : IRequest<Result<IEnumerable<Votes>>>
{
     public int CandidateID { get; }

     public GetVotesByCandidateIDQuery(int candidateID)
     {
          CandidateID = candidateID;
      }
}