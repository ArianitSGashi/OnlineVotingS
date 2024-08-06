using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Persistence.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CampaignRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Campaign>> GetByElectionIdAsync(int electionId)
        {
            return await _dbContext.Campaigns
                                   .Where(c => c.ElectionID == electionId)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Campaign>> GetByCandidateIdAsync(int candidateId)
        {
            return await _dbContext.Campaigns
                                   .Where(c => c.CandidateID == candidateId)
                                   .ToListAsync();
            
        }

        public async Task<IEnumerable<Campaign>> GetActiveCampaignsAsync()
        {
            var currentDate = DateTime.UtcNow; 
            return await _dbContext.Campaigns
                                   .Where(c => c.StartDate <= currentDate && c.EndDate >= currentDate)
                                   .ToListAsync();
        }
    }
}
