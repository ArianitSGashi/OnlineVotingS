using MediatR;
using FluentResults;
using OnlineVotingS.Application.DTO.PostDTO;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class CreateResultCommand : IRequest<Result<ResultEntity>>
{
    public ResultPostDTO ResultDto { get; }

    public CreateResultCommand(ResultPostDTO resultDto)
    {
        ResultDto = resultDto;
    }
}
