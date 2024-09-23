using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Commands;

public class UpdateCampaignHandler : IRequestHandler<UpdateCampaignCommand, FluentResults.Result<Campaign>>
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

    public async Task<FluentResults.Result<Campaign>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignDto.CampaignID);

            if (campaign == null)
            {
                _logger.LogWarning("Campaign with ID {CampaignId} not found.", request.CampaignDto.CampaignID);
                return FluentResults.Result.Fail($"Campaign with ID {request.CampaignDto.CampaignID} not found.");
            }

            _mapper.Map(request.CampaignDto, campaign);
            await _campaignRepository.UpdateAsync(campaign);
            return FluentResults.Result.Ok(campaign);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the campaign with ID {CampaignId}: {ErrorMessage}", request.CampaignDto.CampaignID, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
