using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces;

public interface IVotesRepository : IGenericRepository<Votes>
{
    Task<IEnumerable<Votes>> GetByUserIDAsync(string userID);

    Task<IEnumerable<Votes>> GetByElectionIDAsync(int electionID);

    Task<IEnumerable<Votes>> GetByCandidateIDAsync(int candidateID);

    Task<IEnumerable<Votes>> GetRecentVotesAsync(DateTime date);

    Task<bool> HasUserVotedInElectionAsync(string userId, int electionId);
}
