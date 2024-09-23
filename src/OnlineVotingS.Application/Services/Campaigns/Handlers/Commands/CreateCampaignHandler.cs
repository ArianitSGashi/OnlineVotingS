using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Commands;

public class CreateCampaignHandler : IRequestHandler<CreateCampaignCommand, FluentResults.Result<Campaign>>
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

    public async Task<FluentResults.Result<Campaign>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var campaign = _mapper.Map<Campaign>(request.CampaignDto);
            await _campaignRepository.AddAsync(campaign);
            return FluentResults.Result.Ok(campaign);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a campaign: {ErrorMessage}", ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
