using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        // Retrieves campaigns by a specific election ID
        Task<IEnumerable<Campaign>> GetByElectionIdAsync(int electionId);

        // Retrieves campaigns by a specific candidate ID
        Task<IEnumerable<Campaign>> GetByCandidateIdAsync(int candidateId);

        // Retrieves campaigns that are currently active (i.e., the current date falls between StartDate and EndDate)
        Task<IEnumerable<Campaign>> GetActiveCampaignsAsync();
    }
}
