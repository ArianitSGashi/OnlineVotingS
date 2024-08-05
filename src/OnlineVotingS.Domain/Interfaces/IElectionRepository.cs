using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces
{
    // Inherit from the generic repository interface for Elections entity.
    public interface IElectionRepository : IGenericRepository<Elections>
    {
        // Retrieves elections that are currently active (i.e., the current date falls between StartDate and EndDate)
        Task<IEnumerable<Elections>> GetActiveElectionsAsync();

        // Retrieves elections by a specific title
        Task<IEnumerable<Elections>> GetByTitleAsync(string title);

        // Retrieves elections that start after a specified date
        Task<IEnumerable<Elections>> GetUpcomingElectionsAsync(DateTime date);
    }
}
