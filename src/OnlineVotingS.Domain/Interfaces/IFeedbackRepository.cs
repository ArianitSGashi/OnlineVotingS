using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetByUserIDAsync(string userID);

        Task<IEnumerable<Feedback>> GetByElectionIDAsync(int electionID);

        Task<IEnumerable<Feedback>> GetRecentFeedbacksAsync(DateTime date);
    }

}
