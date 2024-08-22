using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Commands;

public class CreateCampaignCommand : IRequest<Campaign>
{
    public CampaignPostDTO CampaignDto { get;}

    public CreateCampaignCommand(CampaignPostDTO campaignDto)
    {
        CampaignDto = campaignDto;
    }
}