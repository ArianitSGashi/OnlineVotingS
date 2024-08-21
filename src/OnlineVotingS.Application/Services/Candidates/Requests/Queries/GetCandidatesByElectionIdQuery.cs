using MediatR;
using System.Collections.Generic;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidates.Requests.Queries;

public class GetCandidatesByElectionIdQuery : IRequest<IEnumerable<Candidates>>
{
    public int ElectionId { get; }

    public GetCandidatesByElectionIdQuery(int electionId)
    {
        ElectionId = electionId;
    }
}
