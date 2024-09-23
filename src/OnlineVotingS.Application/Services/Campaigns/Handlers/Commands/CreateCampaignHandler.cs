using static FluentResults.Result;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using FluentResults;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Commands;

public class CreateCampaignHandler : IRequestHandler<CreateCampaignCommand, Result<Campaign>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCampaignHandler> _logger;

    public CreateCampaignHandler(ICampaignRepository campaignRepository, IMapper mapper, ILogger<CreateCampaignHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<Campaign>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var campaign = _mapper.Map<Campaign>(request.CampaignDto);
            await _campaignRepository.AddAsync(campaign);
            return Ok(campaign);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a campaign: {ErrorMessage}", ex.Message);
            return new Result<Campaign>().WithError(ErrorCodes.CAMPAIGN_CREATION_FAILED.ToString());
        }
    }
}
