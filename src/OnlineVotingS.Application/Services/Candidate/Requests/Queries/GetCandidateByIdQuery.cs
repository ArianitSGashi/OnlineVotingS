using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidateByIdQuery : IRequest<Candidates>
{
    public int CandidateId { get; set; }

    public GetCandidateByIdQuery(int candidateId)
    {
        CandidateId = candidateId;
    }
}
