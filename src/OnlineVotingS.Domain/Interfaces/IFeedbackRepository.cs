using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Enums;

namespace OnlineVotingS.Domain.Interfaces;

public interface IFeedbackRepository : IGenericRepository<Feedback>
{
    Task<IEnumerable<Feedback>> GetByCategoryAsync(FeedbackCategory category);

    Task<IEnumerable<Feedback>> GetRecentFeedbacksAsync(DateTime date);
}