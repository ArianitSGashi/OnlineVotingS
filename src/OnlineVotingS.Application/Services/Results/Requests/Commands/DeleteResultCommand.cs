using MediatR;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class DeleteResultCommand : IRequest<bool>
{
    public int ResultId { get; }

    public DeleteResultCommand(int resultId)
    {
        ResultId = resultId;
    }
}