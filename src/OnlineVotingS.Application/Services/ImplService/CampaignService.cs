using AutoMapper;
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

        public CampaignService(ICampaignRepository campaignRepository, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task<Campaign> CreateCampaignAsync(CampaignPostDTO campaignDto)
        {
            var campaign = _mapper.Map<Campaign>(campaignDto);
            await _campaignRepository.AddAsync(campaign);
            return campaign;
        }

        public async Task<Campaign> UpdateCampaignAsync(CampaignPutDTO campaignDto)
        {
            var campaign = await _campaignRepository.GetByIdAsync(campaignDto.CampaignID);
            if (campaign == null)
            {
                throw new KeyNotFoundException($"Campaign with ID {campaignDto.CampaignID} not found.");
            }

            _mapper.Map(campaignDto, campaign);
            await _campaignRepository.UpdateAsync(campaign);
            return campaign;
        }

        public async Task<bool> DeleteCampaignAsync(int campaignId)
        {
            var exists = await _campaignRepository.ExistsAsync(campaignId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");
            }

            await _campaignRepository.DeleteAsync(campaignId);
            return true;
        }

        public async Task<Campaign> GetCampaignByIdAsync(int campaignId)
        {
            return await _campaignRepository.GetByIdAsync(campaignId);
        }

        public async Task<IEnumerable<Campaign>> GetAllCampaignsAsync()
        {
            return await _campaignRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsByElectionIdAsync(int electionId)
        {
            return await _campaignRepository.GetByElectionIdAsync(electionId);
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsByCandidateIdAsync(int candidateId)
        {
            return await _campaignRepository.GetByCandidateIdAsync(candidateId);
        }

        public async Task<IEnumerable<Campaign>> GetActiveCampaignsAsync()
        {
            return await _campaignRepository.GetActiveCampaignsAsync();
        }
    }