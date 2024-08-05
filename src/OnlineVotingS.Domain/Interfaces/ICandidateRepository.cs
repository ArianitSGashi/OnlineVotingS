using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces
{
    // Inherit from the generic repository interface for Candidate entity.
    public interface ICandidateRepository : IGenericRepository<Candidates>
    {
        // Retrieves candidates by a specific election ID
        Task<IEnumerable<Candidates>> GetByElectionIdAsync(int electionId);

        // Retrieves candidates by their political party
        Task<IEnumerable<Candidates>> GetByPartyAsync(string party);

        // Retrieves candidates with income greater than a specified amount
        Task<IEnumerable<Candidates>> GetByMinIncomeAsync(decimal minIncome);

        // Retrieves candidates by their name
        Task<IEnumerable<Candidates>> GetByNameAsync(string name);
    }
}
