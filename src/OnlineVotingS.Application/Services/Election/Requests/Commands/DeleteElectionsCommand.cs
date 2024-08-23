using MediatR;

namespace OnlineVotingS.Application.Services.Election.Requests.Commands;

public class DeleteElectionsCommand : IRequest<bool>
{
    public int ElectionId { get; }

    public DeleteElectionsCommand(int electionId)
    {
        ElectionId = electionId;
    }
}