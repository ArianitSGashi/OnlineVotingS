using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces;

public interface ICampaignRepository : IGenericRepository<Campaign>
{
    Task<IEnumerable<Campaign>> GetByElectionIdAsync(int electionId);

    Task<IEnumerable<Campaign>> GetByCandidateIdAsync(int candidateId);

    Task<IEnumerable<Campaign>> GetActiveCampaignsAsync();
}
