using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Requests.Commands;

public class CreateRepliedComplaintCommand : IRequest<RepliedComplaintsPutDTO>
{
    public RepliedComplaintsPostDTO RepliedComplaintDto { get; }

    public CreateRepliedComplaintCommand(RepliedComplaintsPostDTO repliedComplaintDto)
    {
        RepliedComplaintDto = repliedComplaintDto;
    }
}