using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class UpdateResultCommand : IRequest<Result<ResultEntity>>
{
    public ResultPutDTO ResultDto { get; }

    public UpdateResultCommand(ResultPutDTO resultDto)
    {
        ResultDto = resultDto;
    }
}
