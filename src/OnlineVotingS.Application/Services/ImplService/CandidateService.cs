using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.ImplService;

public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CandidateService> _logger;

    public CandidateService(ICandidateRepository candidateRepository, IMapper mapper, ILogger<CandidateService> logger)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Candidates> CreateCandidateAsync(CandidatesPostDTO candidateDto)
    {
        try
        {
            var candidate = _mapper.Map<Candidates>(candidateDto);
            await _candidateRepository.AddAsync(candidate);

            _logger.LogInformation("Candidate created successfully with ID {CandidateId}.", candidate.CandidateID);
            return candidate;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a candidate: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<Candidates> UpdateCandidateAsync(CandidatesPutDTO candidateDto)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(candidateDto.CandidateID);
            if (candidate == null)
            {
                _logger.LogWarning("Candidate with ID {CandidateId} not found.", candidateDto.CandidateID);
                throw new KeyNotFoundException($"Candidate with ID {candidateDto.CandidateID} not found.");
            }

            _mapper.Map(candidateDto, candidate);
            await _candidateRepository.UpdateAsync(candidate);

            _logger.LogInformation("Candidate with ID {CandidateId} updated successfully.", candidateDto.CandidateID);
            return candidate;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the candidate with ID {CandidateId}: {ErrorMessage}", candidateDto.CandidateID, ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteCandidateAsync(int candidateId)
    {
        try
        {
            var exists = await _candidateRepository.ExistsAsync(candidateId);
            if (!exists)
            {
                _logger.LogWarning("Candidate with ID {CandidateId} not found.", candidateId);
                throw new KeyNotFoundException($"Candidate with ID {candidateId} not found.");
            }

            await _candidateRepository.DeleteAsync(candidateId);

            _logger.LogInformation("Candidate with ID {CandidateId} deleted successfully.", candidateId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the candidate with ID {CandidateId}: {ErrorMessage}", candidateId, ex.Message);
            throw;
        }
    }

    public async Task<Candidates> GetCandidateByIdAsync(int candidateId)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(candidateId);
            if (candidate == null)
            {
                _logger.LogWarning("Candidate with ID {CandidateId} not found.", candidateId);
                throw new KeyNotFoundException($"Candidate with ID {candidateId} not found.");
            }

            return candidate;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the candidate with ID {CandidateId}: {ErrorMessage}", candidateId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Candidates>> GetAllCandidatesAsync()
    {
        try
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all candidates: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesByElectionIdAsync(int electionId)
    {
        try
        {
            var candidates = await _candidateRepository.GetByElectionIdAsync(electionId);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching candidates for election ID {ElectionId}: {ErrorMessage}", electionId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesByPartyAsync(string party)
    {
        try
        {
            var candidates = await _candidateRepository.GetByPartyAsync(party);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching candidates for party {Party}: {ErrorMessage}", party, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesByMinIncomeAsync(decimal minIncome)
    {
        try
        {
            var candidates = await _candidateRepository.GetByMinIncomeAsync(minIncome);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching candidates with a minimum income of {MinIncome}: {ErrorMessage}", minIncome, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Candidates>> GetCandidatesByNameAsync(string name)
    {
        try
        {
            var candidates = await _candidateRepository.GetByNameAsync(name);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching candidates with the name {Name}: {ErrorMessage}", name, ex.Message);
            throw;
        }
    }
}
