using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class UpdateResultCommand : IRequest<Result>
{
    public ResultPutDTO ResultDto { get; }

    public UpdateResultCommand(ResultPutDTO resultDto)
    {
        ResultDto = resultDto;
    }
}