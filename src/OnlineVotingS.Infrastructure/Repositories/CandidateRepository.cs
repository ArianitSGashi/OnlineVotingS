using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineVotingS.Infrastructure.Utilities;

namespace OnlineVotingS.Infrastructure.Repositories;

public class CandidateRepository : GenericRepository<Candidates>, ICandidateRepository
{
    public CandidateRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task AddAsync(Candidates entity)
    {
        entity.Party = AesEncryption.Encrypt(entity.Party);
        entity.Works = AesEncryption.Encrypt(entity.Works);
        await base.AddAsync(entity);
    }

    public override Task UpdateAsync(Candidates entity)
    {
        if (entity.Party != null)
        {
            AesEncryption.Encrypt(entity.Party);
        }
        if (entity.Works != null)
        {
            entity.Works = AesEncryption.Encrypt(entity.Works);
        }

        return base.UpdateAsync(entity);
    }

    public override async Task<IEnumerable<Candidates>> GetAllAsync()
    {
        var candidates = await _dbSet.ToListAsync();

        foreach (var candidate in candidates)
        {
            candidate.Party = AesEncryption.Decrypt(candidate.Party);
            candidate.Works = AesEncryption.Decrypt(candidate.Works);
        }

        return candidates;
    }

    public override async Task<Candidates> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.CandidateID == id);
        entity.Party = AesEncryption.Decrypt(entity.Party);
        entity.Works = AesEncryption.Decrypt(entity.Works);
        return entity;
    }

    public async Task<IEnumerable<Candidates>> GetByElectionIdAsync(int electionId)
    {
        var candidates = await _dbSet.Where(c => c.ElectionID == electionId).ToListAsync();

        foreach (var candidate in candidates)
        {
            candidate.Party = AesEncryption.Decrypt(candidate.Party);
            candidate.Works = AesEncryption.Decrypt(candidate.Works);
        }

        return candidates;
    }

    public async Task<IEnumerable<Candidates>> GetByPartyAsync(string party)
    {
        var candidates = await _dbSet.Where(c => c.Party == party).ToListAsync();;

        foreach (var candidate in candidates)
        {
            candidate.Party = AesEncryption.Decrypt(candidate.Party);
            candidate.Works = AesEncryption.Decrypt(candidate.Works);
        }

        return candidates;
    }

    public async Task<IEnumerable<Candidates>> GetByMinIncomeAsync(decimal minIncome)
    {
        var candidates =  await _dbSet.Where(c => c.Income >= minIncome).ToListAsync();

        foreach (var candidate in candidates)
        {
            candidate.Party = AesEncryption.Decrypt(candidate.Party);
            candidate.Works = AesEncryption.Decrypt(candidate.Works);
        }

        return candidates;
    }

    public async Task<IEnumerable<Candidates>> GetByNameAsync(string name)
    {
        var candidates =  await _dbSet.Where(c => c.FullName.Contains(name)).ToListAsync();

        foreach (var candidate in candidates)
        {
            candidate.Party = AesEncryption.Decrypt(candidate.Party);
            candidate.Works = AesEncryption.Decrypt(candidate.Works);
        }

        return candidates;
    }

    public async Task<bool> CandidateBelongsToElectionAsync(int candidateId, int electionId)
    {
        return await _dbSet.AnyAsync(c => c.CandidateID == candidateId && c.ElectionID == electionId);
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesPaginatedAsync(int pageNumber, int pageSize)
    {
        var candidates =  await _context.Candidates
            .OrderBy(c => c.CandidateID) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        foreach (var candidate in candidates)
        {
            candidate.Party = AesEncryption.Decrypt(candidate.Party);
            candidate.Works = AesEncryption.Decrypt(candidate.Works);
        }

        return candidates;
    }

    public async Task<int> GetTotalCandidatesCountAsync()
    {
        return await _context.Candidates.CountAsync();
    }



}
