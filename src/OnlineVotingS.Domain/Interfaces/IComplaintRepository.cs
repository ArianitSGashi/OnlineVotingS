using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces
{
    // Inherit from the generic repository interface for Complaints entity.
    public interface IComplaintRepository : IGenericRepository<Complaints>
    {
        // Retrieves complaints by a specific user ID
        Task<IEnumerable<Complaints>> GetByUserIdAsync(int userId);

        // Retrieves complaints related to a specific election ID
        Task<IEnumerable<Complaints>> GetByElectionIdAsync(int electionId);

        // Retrieves complaints that were filed on or after a specific date
        Task<IEnumerable<Complaints>> GetByComplaintDateAsync(DateTime date);
    }
}
