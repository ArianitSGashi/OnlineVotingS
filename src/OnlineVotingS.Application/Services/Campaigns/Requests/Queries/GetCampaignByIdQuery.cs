using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Queries;

public class GetCampaignByIdQuery : IRequest<Campaign>
{
    public int CampaignID { get; }

    public GetCampaignByIdQuery(int campaignId)
    {
        CampaignID = campaignId;
    }
}