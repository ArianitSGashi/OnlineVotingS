using MediatR;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class GenerateOrUpdateResultsCommand : IRequest<Unit>
{
    public int ElectionID { get; set; }
    public int? CandidateID { get; set; }

    public GenerateOrUpdateResultsCommand(int electionId, int? candidateId = null)
    {
        ElectionID = electionId;
        CandidateID = candidateId;
    }
}