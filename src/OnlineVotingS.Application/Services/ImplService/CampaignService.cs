using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.ImplService;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CampaignService> _logger;

    public CampaignService(ICampaignRepository campaignRepository, IMapper mapper, ILogger<CampaignService> logger)
    {
        _campaignRepository = campaignRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Campaign> CreateCampaignAsync(CampaignPostDTO campaignDto)
    {
        _logger.LogInformation("Creating a new campaign.");

        var campaign = _mapper.Map<Campaign>(campaignDto);
        await _campaignRepository.AddAsync(campaign);

        _logger.LogInformation("Campaign created successfully with ID {CampaignId}.", campaign.CampaignID);
        return campaign;
    }

    public async Task<Campaign> UpdateCampaignAsync(CampaignPutDTO campaignDto)
    {
        _logger.LogInformation("Updating campaign with ID {CampaignId}.", campaignDto.CampaignID);

        var campaign = await _campaignRepository.GetByIdAsync(campaignDto.CampaignID);
        if (campaign == null)
        {
            _logger.LogWarning("Campaign with ID {CampaignId} not found.", campaignDto.CampaignID);
            throw new KeyNotFoundException($"Campaign with ID {campaignDto.CampaignID} not found.");
        }

        _mapper.Map(campaignDto, campaign);
        await _campaignRepository.UpdateAsync(campaign);

        _logger.LogInformation("Campaign with ID {CampaignId} updated successfully.", campaignDto.CampaignID);
        return campaign;
    }

    public async Task<bool> DeleteCampaignAsync(int campaignId)
    {
        _logger.LogInformation("Deleting campaign with ID {CampaignId}.", campaignId);

        var exists = await _campaignRepository.ExistsAsync(campaignId);
        if (!exists)
        {
            _logger.LogWarning("Campaign with ID {CampaignId} not found.", campaignId);
            throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");
        }

        await _campaignRepository.DeleteAsync(campaignId);

        _logger.LogInformation("Campaign with ID {CampaignId} deleted successfully.", campaignId);
        return true;
    }

    public async Task<Campaign> GetCampaignByIdAsync(int campaignId)
    {
        _logger.LogInformation("Fetching campaign with ID {CampaignId}.", campaignId);

        var campaign = await _campaignRepository.GetByIdAsync(campaignId);
        if (campaign == null)
        {
            _logger.LogWarning("Campaign with ID {CampaignId} not found.", campaignId);
            throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");
        }

        return campaign;
    }

    public async Task<IEnumerable<Campaign>> GetAllCampaignsAsync()
    {
        _logger.LogInformation("Fetching all campaigns.");

        var campaigns = await _campaignRepository.GetAllAsync();

        _logger.LogInformation("Fetched {Count} campaigns.", campaigns.Count());
        return campaigns;
    }

    public async Task<IEnumerable<Campaign>> GetCampaignsByElectionIdAsync(int electionId)
    {
        _logger.LogInformation("Fetching campaigns for election ID {ElectionId}.", electionId);

        var campaigns = await _campaignRepository.GetByElectionIdAsync(electionId);

        _logger.LogInformation("Fetched {Count} campaigns for election ID {ElectionId}.", campaigns.Count(), electionId);
        return campaigns;
    }

    public async Task<IEnumerable<Campaign>> GetCampaignsByCandidateIdAsync(int candidateId)
    {
        _logger.LogInformation("Fetching campaigns for candidate ID {CandidateId}.", candidateId);

        var campaigns = await _campaignRepository.GetByCandidateIdAsync(candidateId);

        _logger.LogInformation("Fetched {Count} campaigns for candidate ID {CandidateId}.", campaigns.Count(), candidateId);
        return campaigns;
    }

    public async Task<IEnumerable<Campaign>> GetActiveCampaignsAsync()
    {
        _logger.LogInformation("Fetching all active campaigns.");

        var activeCampaigns = await _campaignRepository.GetActiveCampaignsAsync();

        _logger.LogInformation("Fetched {Count} active campaigns.", activeCampaigns.Count());
        return activeCampaigns;
    }
}