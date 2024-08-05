using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Domain.Interfaces
{
    public interface IResultRepository : IGenericRepository<Result>
    {
        Task<IEnumerable<Result>> GetByElectionIdAsync(int electionId);

        Task<IEnumerable<Result>> GetByCandidateIdAsync(int candidateId);

        Task<IEnumerable<Result>> GetByTotalVotesGreaterThanAsync(int votes);
    }
}
