using MediatR;
using FluentResults;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Results.Requests.Commands;

public class CreateResultCommand : IRequest<FluentResults.Result<OnlineVotingS.Domain.Entities.Result>>
{
    public ResultPostDTO ResultDto { get; }

    public CreateResultCommand(ResultPostDTO resultDto)
    {
        ResultDto = resultDto;
    }
}
