using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Commands;

public class CreateCandidateCommand : IRequest<Candidates>
{
    public CandidatesPostDTO CandidateDto { get; }

    public CreateCandidateCommand(CandidatesPostDTO candidateDto)
    {
        CandidateDto = candidateDto;
    }
}