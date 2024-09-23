using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Commands;

public class DeleteCampaignHandler : IRequestHandler<DeleteCampaignCommand, FluentResults.Result>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<DeleteCampaignHandler> _logger;

    public DeleteCampaignHandler(ICampaignRepository campaignRepository, ILogger<DeleteCampaignHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<FluentResults.Result> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _campaignRepository.ExistsAsync(request.CampaignId);
            if (!exists)
            {
                return FluentResults.Result.Fail($"Campaign with ID {request.CampaignId} not found.");
            }

            await _campaignRepository.DeleteAsync(request.CampaignId);

            return FluentResults.Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the campaign with ID {CampaignId}: {ErrorMessage}", request.CampaignId, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
