using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Commands;

public class UpdateCandidateCommand : IRequest<Candidates>
{
    public CandidatesPutDTO CandidateDto { get; }

    public UpdateCandidateCommand(CandidatesPutDTO candidateDto)
    {
        CandidateDto = candidateDto;
    }
}
