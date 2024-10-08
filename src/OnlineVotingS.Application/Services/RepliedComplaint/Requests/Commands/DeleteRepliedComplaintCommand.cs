﻿using FluentResults;
using MediatR;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;

public class DeleteRepliedComplaintCommand : IRequest<Result<bool>>
{
    public int RepliedComplaintId { get; }

    public DeleteRepliedComplaintCommand(int repliedComplaintId)
    {
        RepliedComplaintId = repliedComplaintId;
    }
}
