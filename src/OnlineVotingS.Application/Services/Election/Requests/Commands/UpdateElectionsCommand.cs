using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Election.Requests.Commands;

public class UpdateElectionsCommand : IRequest<Elections>
{
    public ElectionsPutDTO ElectionDto { get; }

    public UpdateElectionsCommand(ElectionsPutDTO electionDto)
    {
        ElectionDto = electionDto;
    }
}