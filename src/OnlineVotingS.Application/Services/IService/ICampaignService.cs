using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.IService;

public interface ICampaignService
{
    Task<Campaign> CreateCampaignAsync(CampaignPostDTO campaignDto);
    Task<Campaign> UpdateCampaignAsync(CampaignPutDTO campaignDto);
    Task<bool> DeleteCampaignAsync(int campaignId);
    Task<Campaign> GetCampaignByIdAsync(int campaignId);
    Task<IEnumerable<Campaign>> GetAllCampaignsAsync();
    Task<IEnumerable<Campaign>> GetCampaignsByElectionIdAsync(int electionId);
    Task<IEnumerable<Campaign>> GetCampaignsByCandidateIdAsync(int candidateId);
    Task<IEnumerable<Campaign>> GetActiveCampaignsAsync();
}