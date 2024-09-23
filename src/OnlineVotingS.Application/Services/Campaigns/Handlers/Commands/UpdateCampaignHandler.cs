using static FluentResults.Result;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Commands;

public class UpdateCampaignHandler : IRequestHandler<UpdateCampaignCommand, Result<Campaign>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCampaignHandler> _logger;

    public UpdateCampaignHandler(ICampaignRepository campaignRepository, IMapper mapper, ILogger<UpdateCampaignHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<Campaign>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignDto.CampaignID);
            if (campaign == null)
            {
                return new Result<Campaign>().WithError(ErrorCodes.CAMPAIGN_NOT_FOUND.ToString());
            }

            _mapper.Map(request.CampaignDto, campaign);
            await _campaignRepository.UpdateAsync(campaign);
            return Ok(campaign);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the campaign with ID {CampaignId}: {ErrorMessage}", request.CampaignDto.CampaignID, ex.Message);
            return new Result<Campaign>().WithError(ErrorCodes.CAMPAIGN_UPDATE_FAILED.ToString());
        }
    }
}
