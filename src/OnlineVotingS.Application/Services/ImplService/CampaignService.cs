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

        try
        {
            var campaign = _mapper.Map<Campaign>(campaignDto);
            await _campaignRepository.AddAsync(campaign);

            _logger.LogInformation("Campaign created successfully with ID {CampaignId}.", campaign.CampaignID);
            return campaign;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a campaign.");
            throw;
        }
    }

    public async Task<Campaign> UpdateCampaignAsync(CampaignPutDTO campaignDto)
    {
        _logger.LogInformation("Updating campaign with ID {CampaignId}.", campaignDto.CampaignID);

        try
        {
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the campaign with ID {CampaignId}.", campaignDto.CampaignID);
            throw;
        }
    }

    public async Task<bool> DeleteCampaignAsync(int campaignId)
    {
        _logger.LogInformation("Deleting campaign with ID {CampaignId}.", campaignId);

        try
        {
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the campaign with ID {CampaignId}.", campaignId);
            throw;
        }
    }

    public async Task<Campaign> GetCampaignByIdAsync(int campaignId)
    {
        _logger.LogInformation("Fetching campaign with ID {CampaignId}.", campaignId);

        try
        {
            var campaign = await _campaignRepository.GetByIdAsync(campaignId);
            if (campaign == null)
            {
                _logger.LogWarning("Campaign with ID {CampaignId} not found.", campaignId);
                throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");
            }

            return campaign;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the campaign with ID {CampaignId}.", campaignId);
            throw;
        }
    }

    public async Task<IEnumerable<Campaign>> GetAllCampaignsAsync()
    {
        _logger.LogInformation("Fetching all campaigns.");

        try
        {
            var campaigns = await _campaignRepository.GetAllAsync();
            _logger.LogInformation("Fetched {Count} campaigns.", campaigns.Count());
            return campaigns;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all campaigns.");
            throw;
        }
    }

    public async Task<IEnumerable<Campaign>> GetCampaignsByElectionIdAsync(int electionId)
    {
        _logger.LogInformation("Fetching campaigns for election ID {ElectionId}.", electionId);

        try
        {
            var campaigns = await _campaignRepository.GetByElectionIdAsync(electionId);
            _logger.LogInformation("Fetched {Count} campaigns for election ID {ElectionId}.", campaigns.Count(), electionId);
            return campaigns;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching campaigns for election ID {ElectionId}.", electionId);
            throw;
        }
    }

    public async Task<IEnumerable<Campaign>> GetCampaignsByCandidateIdAsync(int candidateId)
    {
        _logger.LogInformation("Fetching campaigns for candidate ID {CandidateId}.", candidateId);

        try
        {
            var campaigns = await _campaignRepository.GetByCandidateIdAsync(candidateId);
            _logger.LogInformation("Fetched {Count} campaigns for candidate ID {CandidateId}.", campaigns.Count(), candidateId);
            return campaigns;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching campaigns for candidate ID {CandidateId}.", candidateId);
            throw;
        }
    }

    public async Task<IEnumerable<Campaign>> GetActiveCampaignsAsync()
    {
        _logger.LogInformation("Fetching all active campaigns.");

        try
        {
            var activeCampaigns = await _campaignRepository.GetActiveCampaignsAsync();
            _logger.LogInformation("Fetched {Count} active campaigns.", activeCampaigns.Count());
            return activeCampaigns;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching active campaigns.");
            throw;
        }
    }
}