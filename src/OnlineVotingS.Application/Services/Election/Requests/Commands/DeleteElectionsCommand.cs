using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Election.Requests.Commands;

public class DeleteElectionsCommand : IRequest<bool>
    {
        public int ElectionId { get; }

        public DeleteElectionsCommand(int electionId)
        {
        ElectionId = electionId;
        }
    }