using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces
{
    public interface IResultRepository : IGenericRepository<Result>
    {
        Task<IEnumerable<Result>> GetByElectionIDAsync(int electionID);

        Task<IEnumerable<Result>> GetByCandidateIDAsync(int candidateID);

        Task<IEnumerable<Result>> GetRecentResultsAsync(DateTime date);
    }
}
