﻿using FluentResults;
using MediatR;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;

public class DeleteFeedbackCommand : IRequest<Result<bool>>
{
    public int FeedbackId { get; }

    public DeleteFeedbackCommand(int feedbackId)
    {
        FeedbackId = feedbackId;
    }
}
