using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetAllCampaignsHandler : IRequestHandler<GetAllCampaignsQuery, IEnumerable<Campaign>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetAllCampaignsHandler> _logger;

    public GetAllCampaignsHandler(ICampaignRepository campaignRepository, ILogger<GetAllCampaignsHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Campaign>> Handle(GetAllCampaignsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _campaignRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all campaigns: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}