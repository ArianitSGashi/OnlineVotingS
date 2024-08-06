using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces;

public interface IComplaintRepository : IGenericRepository<Complaints>
{
    Task<IEnumerable<Complaints>> GetByUserIdAsync(int userId);

    Task<IEnumerable<Complaints>> GetByElectionIdAsync(int electionId);

    Task<IEnumerable<Complaints>> GetByComplaintDateAsync(DateTime date);
}
