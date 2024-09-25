using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetCampaignsByCandidateIdHandler : IRequestHandler<GetCampaignsByCandidateIdQuery, Result<IEnumerable<Campaign>>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetCampaignsByCandidateIdHandler> _logger;

    public GetCampaignsByCandidateIdHandler(ICampaignRepository campaignRepository, ILogger<GetCampaignsByCandidateIdHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Campaign>>> Handle(GetCampaignsByCandidateIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var campaigns = await _campaignRepository.GetByCandidateIdAsync(request.CandidateID);
            return Ok(campaigns);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching campaigns for candidate ID {CandidateId}: {ErrorMessage}", request.CandidateID, ex.Message);
            return new Result<IEnumerable<Campaign>>().WithError(ErrorCodes.CAMPAIGN_NOT_FOUND.ToString());
        }
    }
}