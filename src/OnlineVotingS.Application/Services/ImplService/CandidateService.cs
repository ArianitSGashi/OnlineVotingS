using AutoMapper;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.ImplService;

public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;

    public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
    }

    public async Task<Candidates> CreateCandidateAsync(CandidatesPostDTO candidateDto)
    {
        var candidate = _mapper.Map<Candidates>(candidateDto);
        await _candidateRepository.AddAsync(candidate);
        return candidate;
    }

    public async Task<Candidates> UpdateCandidateAsync(CandidatesPutDTO candidateDto)
    {
        var candidate = await _candidateRepository.GetByIdAsync(candidateDto.CandidateID);
        if (candidate == null)
        {
            throw new KeyNotFoundException($"Candidate with ID {candidateDto.CandidateID} not found.");
        }

        _mapper.Map(candidateDto, candidate);
        await _candidateRepository.UpdateAsync(candidate);
        return candidate;
    }

    public async Task<bool> DeleteCandidateAsync(int candidateId)
    {
        var exists = await _candidateRepository.ExistsAsync(candidateId);
        if (!exists)
        {
            throw new KeyNotFoundException($"Candidate with ID {candidateId} not found.");
        }

        await _candidateRepository.DeleteAsync(candidateId);
        return true;
    }

    public async Task<Candidates> GetCandidateByIdAsync(int candidateId)
    {
        return await _candidateRepository.GetByIdAsync(candidateId);
    }

    public async Task<IEnumerable<Candidates>> GetAllCandidatesAsync()
    {
        return await _candidateRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesByElectionIdAsync(int electionId)
    {
        return await _candidateRepository.GetByElectionIdAsync(electionId);
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesByPartyAsync(string party)
    {
        return await _candidateRepository.GetByPartyAsync(party);
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesByMinIncomeAsync(decimal minIncome)
    {
        return await _candidateRepository.GetByMinIncomeAsync(minIncome);
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesByNameAsync(string name)
    {
        return await _candidateRepository.GetByNameAsync(name);
    }
}
