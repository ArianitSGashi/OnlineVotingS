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
    public async Task<int> GetTotalElectionsCountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<IEnumerable<Elections>> GetElectionsPaginatedAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .OrderBy(e => e.ElectionID) // Ensure consistent ordering
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Elections>> GetActiveElectionsAsync()
    {
        var now = DateTime.Now;
        var currentDate = DateOnly.FromDateTime(now);
        var currentTime = now.TimeOfDay;

        return await _dbSet.Where(e =>
            (e.Status == ElectionStatus.Active) ||
            (e.Status != ElectionStatus.Completed &&
             ((e.StartDate < currentDate) || (e.StartDate == currentDate && e.StartTime <= currentTime)) &&
             ((e.EndDate > currentDate) || (e.EndDate == currentDate && e.EndTime >= currentTime)))
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
            e.Status == ElectionStatus.Not_Active &&
            (e.StartDate > queryDate || (e.StartDate == queryDate && e.StartTime > queryTime))
        ).ToListAsync();
    }

    public async Task<IEnumerable<Elections>> GetCompletableElectionsAsync()
    {
        var now = DateTime.Now;
        var currentDate = DateOnly.FromDateTime(now);
        var currentTime = now.TimeOfDay;

        return await _dbSet.Where(e =>
            e.Status == ElectionStatus.Active ||
            (e.Status != ElectionStatus.Completed &&
             ((e.EndDate < currentDate) || (e.EndDate == currentDate && e.EndTime <= currentTime)))
        ).ToListAsync();
    }
}