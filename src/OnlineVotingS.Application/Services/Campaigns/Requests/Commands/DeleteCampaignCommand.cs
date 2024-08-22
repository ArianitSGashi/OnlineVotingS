﻿using MediatR;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Commands;

public class DeleteCampaignCommand : IRequest<bool>
{
    public int CampaignId { get; }

    public DeleteCampaignCommand(int campaignId)
    {
        CampaignId = campaignId;
    }
}