using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidates.Requests.Commands;

public class UpdateCandidateCommand : IRequest<Candidates>
{
    public CandidatesPutDTO CandidateDto { get; }

    public UpdateCandidateCommand(CandidatesPutDTO candidateDto)
    {
        CandidateDto = candidateDto;
    }
}
