using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.Infrastructure.Repositories
{
    public class ElectionRepository : GenericRepository<Elections>, IElectionRepository
    {
        public ElectionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Elections>> GetActiveElectionsAsync()
        {
            return await _dbSet.Where(e => e.StartDate <= DateTime.Now && e.EndDate >= DateTime.Now).ToListAsync();
        }

        public async Task<IEnumerable<Elections>> GetByTitleAsync(string title)
        {
            return await _dbSet.Where(e => e.Title.Contains(title)).ToListAsync();
        }

        public async Task<IEnumerable<Elections>> GetUpcomingElectionsAsync(DateTime date)
        {
            return await _dbSet.Where(e => e.StartDate > date).ToListAsync();
        }
    }
}