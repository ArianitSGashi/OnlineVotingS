using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetActiveCampaignsHandler : IRequestHandler<GetActiveCampaignsQuery, Result<IEnumerable<Campaign>>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetActiveCampaignsHandler> _logger;

    public GetActiveCampaignsHandler(ICampaignRepository campaignRepository, ILogger<GetActiveCampaignsHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Campaign>>> Handle(GetActiveCampaignsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var campaigns = await _campaignRepository.GetActiveCampaignsAsync();
            return Ok(campaigns);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching active campaigns: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<Campaign>>().WithError(ErrorCodes.CAMPAIGN_NOT_FOUND.ToString());
        }
    }
}