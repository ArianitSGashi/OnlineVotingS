using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Commands;

public class UpdateCandidateCommand : IRequest<Result<Candidates>>
{
    public CandidatesPutDTO CandidateDto { get; }

    public UpdateCandidateCommand(CandidatesPutDTO candidateDto)
    {
        CandidateDto = candidateDto;
    }
}
