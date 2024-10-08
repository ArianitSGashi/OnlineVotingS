﻿using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Commands;

public class CreateRepliedComplaintCommand : IRequest<Result<RepliedComplaints>>
{
    public RepliedComplaintsPostDTO RepliedComplaintsPostDTO { get; }

    public CreateRepliedComplaintCommand(RepliedComplaintsPostDTO repliedComplaintsPostDTO)
    {
        RepliedComplaintsPostDTO = repliedComplaintsPostDTO;
    }
}
