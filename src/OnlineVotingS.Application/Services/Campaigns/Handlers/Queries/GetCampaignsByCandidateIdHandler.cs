using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Campaigns.Handlers.Queries;

public class GetCampaignsByCandidateIdHandler : IRequestHandler<GetCampaignsByCandidateIdQuery, IEnumerable<Campaign>>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILogger<GetCampaignsByCandidateIdHandler> _logger;

    public GetCampaignsByCandidateIdHandler(ICampaignRepository campaignRepository, ILogger<GetCampaignsByCandidateIdHandler> logger)
    {
        _campaignRepository = campaignRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Campaign>> Handle(GetCampaignsByCandidateIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _campaignRepository.GetByCandidateIdAsync(request.CandidateID);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching campaigns for candidate ID {CandidateId}: {ErrorMessage}", request.CandidateID, ex.Message);
            throw;
        }
    }
}