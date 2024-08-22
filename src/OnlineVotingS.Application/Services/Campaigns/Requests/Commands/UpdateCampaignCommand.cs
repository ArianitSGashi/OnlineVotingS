using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Commands;

public class UpdateCampaignCommand : IRequest<Campaign>
{
    public CampaignPutDTO CampaignDto { get; }

    public UpdateCampaignCommand(CampaignPutDTO campaignDto)
    {
        CampaignDto = campaignDto;
    }
}