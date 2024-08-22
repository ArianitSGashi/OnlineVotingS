using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetActiveCampaignsHandler : IRequestHandler<GetActiveCampaignsQuery, IEnumerable<Campaign>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetActiveCampaignsHandler> _logger;

    public GetActiveCampaignsHandler(ICampaignRepository campaignRepository, ILogger<GetActiveCampaignsHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Campaign>> Handle(GetActiveCampaignsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _campaignRepository.GetActiveCampaignsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching active campaigns: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}