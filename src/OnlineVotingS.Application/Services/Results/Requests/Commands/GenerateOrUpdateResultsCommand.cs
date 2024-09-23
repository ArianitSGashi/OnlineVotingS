using MediatR;
using FluentResults;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class GenerateOrUpdateResultsCommand : IRequest<Result>
{
    public int ElectionID { get; set; }
    public int? CandidateID { get; set; }

    public GenerateOrUpdateResultsCommand(int electionId, int? candidateId = null)
    {
        ElectionID = electionId;
        CandidateID = candidateId;
    }
}
