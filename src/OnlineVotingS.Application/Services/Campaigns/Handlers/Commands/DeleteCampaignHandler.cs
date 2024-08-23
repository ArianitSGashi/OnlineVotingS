using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Commands;

public class DeleteCampaignHandler : IRequestHandler<DeleteCampaignCommand, bool>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<DeleteCampaignHandler> _logger;

    public DeleteCampaignHandler(ICampaignRepository campaignRepository, ILogger<DeleteCampaignHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _campaignRepository.ExistsAsync(request.CampaignId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Campaign with ID {request.CampaignId} not found.");
            }

            await _campaignRepository.DeleteAsync(request.CampaignId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the campaign with ID {CampaignId}: {ErrorMessage}", request.CampaignId, ex.Message);
            throw;
        }
    }
}