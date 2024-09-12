using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces;

public interface ICandidateRepository : IGenericRepository<Candidates>
{
    Task<IEnumerable<Candidates>> GetByElectionIdAsync(int electionId);

    Task<IEnumerable<Candidates>> GetByPartyAsync(string party);

    Task<IEnumerable<Candidates>> GetByMinIncomeAsync(decimal minIncome);

    Task<IEnumerable<Candidates>> GetByNameAsync(string name);

    Task<bool> CandidateBelongsToElectionAsync(int candidateId, int electionId);
}
