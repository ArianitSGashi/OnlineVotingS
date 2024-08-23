using MediatR;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Commands;

public class DeleteCandidateCommand : IRequest<bool>
{
    public int CandidateId { get; }

    public DeleteCandidateCommand(int candidateId)
    {
        CandidateId = candidateId;
    }
}