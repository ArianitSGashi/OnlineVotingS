﻿using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;

public class CreateRepliedComplaintCommand : IRequest<Result<RepliedComplaints>>
{
    public RepliedComplaintsPostDTO RepliedComplaint { get; }

    public CreateRepliedComplaintCommand(RepliedComplaintsPostDTO repliedComplaint)
    {
        RepliedComplaint = repliedComplaint;
    }
}
