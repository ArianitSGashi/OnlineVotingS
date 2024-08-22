using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Election.Requests.Commands;

public class CreateElectionsCommand : IRequest<Elections>
{
    public ElectionsPostDTO ElectionDto { get; }

    public CreateElectionsCommand(ElectionsPostDTO electionDto)
    {
        ElectionDto = electionDto;
    }
}