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
        try
        {
            var campaign = _mapper.Map<Campaign>(campaignDto);
            await _campaignRepository.AddAsync(campaign);

            _logger.LogInformation("Campaign created successfully with ID {CampaignId}.", campaign.CampaignID);
            return campaign;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a campaign: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<Campaign> UpdateCampaignAsync(CampaignPutDTO campaignDto)
    {
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
            _logger.LogError("An error occurred while updating the campaign with ID {CampaignId}: {ErrorMessage}", campaignDto.CampaignID, ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteCampaignAsync(int campaignId)
    {
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
            _logger.LogError("An error occurred while deleting the campaign with ID {CampaignId}: {ErrorMessage}", campaignId, ex.Message);
            throw;
        }
    }

    public async Task<Campaign> GetCampaignByIdAsync(int campaignId)
    {
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
            _logger.LogError("An error occurred while fetching the campaign with ID {CampaignId}: {ErrorMessage}", campaignId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Campaign>> GetAllCampaignsAsync()
    {
        try
        {
            var campaigns = await _campaignRepository.GetAllAsync();
            return campaigns;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all campaigns: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Campaign>> GetCampaignsByElectionIdAsync(int electionId)
    {
        try
        {
            var campaigns = await _campaignRepository.GetByElectionIdAsync(electionId);
            return campaigns;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching campaigns for election ID {ElectionId}: {ErrorMessage}", electionId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Campaign>> GetCampaignsByCandidateIdAsync(int candidateId)
    {
        try
        {
            var campaigns = await _campaignRepository.GetByCandidateIdAsync(candidateId);
            return campaigns;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching campaigns for candidate ID {CandidateId}: {ErrorMessage}", candidateId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Campaign>> GetActiveCampaignsAsync()
    {
        try
        {
            var activeCampaigns = await _campaignRepository.GetActiveCampaignsAsync();
            return activeCampaigns;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching active campaigns: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}