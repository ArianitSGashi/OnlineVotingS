using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetCampaignsByElectionIdHandler : IRequestHandler<GetCampaignsByElectionIdQuery, Result<IEnumerable<Campaign>>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetCampaignsByElectionIdHandler> _logger;

    public GetCampaignsByElectionIdHandler(ICampaignRepository campaignRepository, ILogger<GetCampaignsByElectionIdHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Campaign>>> Handle(GetCampaignsByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var campaigns = await _campaignRepository.GetByElectionIdAsync(request.ElectionID);
            return Ok(campaigns);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching campaigns for election ID {ElectionId}: {ErrorMessage}", request.ElectionID, ex.Message);
            return new Result<IEnumerable<Campaign>>().WithError(ErrorCodes.CAMPAIGN_NOT_FOUND.ToString());
        }
    }
}