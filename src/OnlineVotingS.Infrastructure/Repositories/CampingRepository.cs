using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Infrastructure.Repositories;

public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
{
    public CampaignRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Campaign>> GetByElectionIdAsync(int electionId)
    {
        return await _dbSet.Where(c => c.ElectionID == electionId).ToListAsync();
    }

    public async Task<IEnumerable<Campaign>> GetByCandidateIdAsync(int candidateId)
    {
        return await _dbSet.Where(c => c.CandidateID == candidateId).ToListAsync();
    }

    public async Task<IEnumerable<Campaign>> GetActiveCampaignsAsync()
    {
        return await _dbSet.Where(c => c.StartDate <= DateTime.Now && c.EndDate >= DateTime.Now).ToListAsync();
    }
}