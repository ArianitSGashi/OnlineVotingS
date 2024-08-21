using MediatR;

namespace OnlineVotingS.Application.Services.Candidates.Requests.Commands;

public class DeleteCandidateCommand : IRequest<bool>
{
    public int CandidateId { get; }

    public DeleteCandidateCommand(int candidateId)
    {
        CandidateId = candidateId;
    }
}
