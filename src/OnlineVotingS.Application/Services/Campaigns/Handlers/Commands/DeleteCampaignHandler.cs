using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Commands;

public class DeleteCampaignHandler : IRequestHandler<DeleteCampaignCommand, Result>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<DeleteCampaignHandler> _logger;

    public DeleteCampaignHandler(ICampaignRepository campaignRepository, ILogger<DeleteCampaignHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _campaignRepository.ExistsAsync(request.CampaignId);
            if (!exists)
            {
                return new Result().WithError(ErrorCodes.CAMPAIGN_NOT_FOUND.ToString());
            }

            await _campaignRepository.DeleteAsync(request.CampaignId);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the campaign with ID {CampaignId}: {ErrorMessage}", request.CampaignId, ex.Message);
            return new Result().WithError(ErrorCodes.CAMPAIGN_DELETION_FAILED.ToString());
        }
    }
}
