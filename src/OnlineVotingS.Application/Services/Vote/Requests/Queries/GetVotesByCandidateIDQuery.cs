using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetVotesByCandidateIDQuery : IRequest<IEnumerable<Votes>>
{
     public int CandidateID { get; }

     public GetVotesByCandidateIDQuery(int candidateID)
     {
          CandidateID = candidateID;
      }
}
