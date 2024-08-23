using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Commands;

public class CreateCampaignHandler : IRequestHandler<CreateCampaignCommand, Campaign>
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

    public async Task<Campaign> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var campaign = _mapper.Map<Campaign>(request.CampaignDto);
            await _campaignRepository.AddAsync(campaign);
            return campaign;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a campaign: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}