using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        // Metoda e re per te marrur feedback bazuar ne Kategori
        Task<IEnumerable<Feedback>> GetByCategoryAsync(FeedbackCategory category);

        // Metoda per te marr feedback bazuar ne date
        Task<IEnumerable<Feedback>> GetRecentFeedbacksAsync(DateTime date);
    }
}