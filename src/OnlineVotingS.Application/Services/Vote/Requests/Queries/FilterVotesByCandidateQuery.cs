using MediatR;
using OnlineVotingS.Application.DTO.GetDTO;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class FilterVotesByCandidateQuery : IRequest<IEnumerable<CandidateVotes>>
{
    public int ElectionId { get; set; }

    public FilterVotesByCandidateQuery(int electionId)
    {
        ElectionId = electionId;
    }
}