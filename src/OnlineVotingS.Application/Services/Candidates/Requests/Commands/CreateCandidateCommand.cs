using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidates.Requests.Commands;

public class CreateCandidateCommand : IRequest<Candidates>
{
    public CandidatesPostDTO CandidateDto { get; }

    public CreateCandidateCommand(CandidatesPostDTO candidateDto)
    {
        CandidateDto = candidateDto;
    }
}
