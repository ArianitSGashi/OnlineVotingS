using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetCampaignByIdHandler : IRequestHandler<GetCampaignByIdQuery, Result<Campaign>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetCampaignByIdHandler> _logger;

    public GetCampaignByIdHandler(ICampaignRepository campaignRepository, ILogger<GetCampaignByIdHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<Result<Campaign>> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignID);
            if (campaign == null)
            {
                return new Result<Campaign>().WithError(ErrorCodes.CAMPAIGN_NOT_FOUND.ToString());
            }
            return Ok(campaign);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the campaign with ID {CampaignId}: {ErrorMessage}", request.CampaignID, ex.Message);
            return new Result<Campaign>().WithError(ErrorCodes.CAMPAIGN_NOT_FOUND.ToString());
        }
    }
}