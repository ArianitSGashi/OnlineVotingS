using MediatR;
using FluentResults;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class GenerateOrUpdateResultsCommand : IRequest<Result<Unit>>
{
    public int ElectionID { get; }
    public int? CandidateID { get; }

    public GenerateOrUpdateResultsCommand(int electionId, int? candidateId = null)
    {
        ElectionID = electionId;
        CandidateID = candidateId;
    }
}