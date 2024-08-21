using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetCampaignByIdHandler : IRequestHandler<GetCampaignByIdQuery, Campaign>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetCampaignByIdHandler> _logger;

    public GetCampaignByIdHandler(ICampaignRepository campaignRepository, ILogger<GetCampaignByIdHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<Campaign> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignID);
            if (campaign == null)
            {
                _logger.LogWarning("Campaign with ID {CampaignId} not found.", request.CampaignID);
                throw new KeyNotFoundException($"Campaign with ID {request.CampaignID} not found.");
            }

            return campaign;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the campaign with ID {CampaignId}: {ErrorMessage}", request.CampaignID, ex.Message);
            throw;
        }
    }
}