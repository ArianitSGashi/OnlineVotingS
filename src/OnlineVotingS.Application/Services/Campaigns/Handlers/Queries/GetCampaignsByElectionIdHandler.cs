using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetCampaignsByElectionIdHandler : IRequestHandler<GetCampaignsByElectionIdQuery, IEnumerable<Campaign>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetCampaignsByElectionIdHandler> _logger;

    public GetCampaignsByElectionIdHandler(ICampaignRepository campaignRepository, ILogger<GetCampaignsByElectionIdHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Campaign>> Handle(GetCampaignsByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _campaignRepository.GetByElectionIdAsync(request.ElectionID);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching campaigns for election ID {ElectionId}: {ErrorMessage}", request.ElectionID, ex.Message);
            throw;
        }
    }
}