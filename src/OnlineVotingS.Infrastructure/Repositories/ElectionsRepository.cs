using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;

namespace OnlineVotingS.Infrastructure.Repositories;

public class ElectionRepository : GenericRepository<Elections>, IElectionRepository
{
    public ElectionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Elections>> GetActiveElectionsAsync()
    {
        var now = DateTime.Now;
        var currentDate = DateOnly.FromDateTime(now);
        var currentTime = now.TimeOfDay;

        return await _dbSet.Where(e =>
            e.Status == ElectionStatus.Active &&
            ((e.StartDate < currentDate) || (e.StartDate == currentDate && e.StartTime <= currentTime)) &&
            ((e.EndDate > currentDate) || (e.EndDate == currentDate && e.EndTime >= currentTime))
        ).ToListAsync();
    }

    public async Task<Elections> GetByTitleAsync(string title)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Title == title);
    }

    public async Task<IEnumerable<Elections>> GetElectionsByTitleAsync(string title)
    {
        return await _dbSet.Where(e => e.Title.Contains(title)).ToListAsync();
    }

    public async Task<IEnumerable<Elections>> GetUpcomingElectionsAsync(DateTime date)
    {
        var queryDate = DateOnly.FromDateTime(date);
        var queryTime = date.TimeOfDay;

        return await _dbSet.Where(e =>
            e.Status == ElectionStatus.Active &&
            (e.StartDate > queryDate || (e.StartDate == queryDate && e.StartTime > queryTime))
        ).ToListAsync();
    }
}