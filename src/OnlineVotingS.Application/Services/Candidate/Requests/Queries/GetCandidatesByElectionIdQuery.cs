using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByElectionIdQuery : IRequest<IEnumerable<Candidates>>
{
    public int ElectionId { get; set; }

    public GetCandidatesByElectionIdQuery(int electionId)
    {
        ElectionId = electionId;
    }
}
