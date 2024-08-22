using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Requests.Commands;

public class UpdateRepliedComplaintCommand : IRequest<RepliedComplaintsPutDTO>
{
    public RepliedComplaintsPutDTO RepliedComplaintDto { get; }

    public UpdateRepliedComplaintCommand(RepliedComplaintsPutDTO repliedComplaintDto)
    {
        RepliedComplaintDto = repliedComplaintDto;
    }
}