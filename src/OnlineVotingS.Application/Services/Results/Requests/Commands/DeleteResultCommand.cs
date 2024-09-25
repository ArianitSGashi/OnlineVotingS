using FluentResults;
using MediatR;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class DeleteResultCommand : IRequest<Result<bool>>
{
    public int ResultId { get; }

    public DeleteResultCommand(int resultId)
    {
        ResultId = resultId;
    }
}
