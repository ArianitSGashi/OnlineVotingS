using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Commands;

public class DeleteCandidateCommand : IRequest<bool>
{
    public int CandidateId { get; }

    public DeleteCandidateCommand(int candidateId)
    {
        CandidateId = candidateId;
    }
}
