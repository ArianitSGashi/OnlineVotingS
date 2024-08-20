using AutoMapper;
using Microsoft.Extensions.Logging;
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

public class ElectionsService : IElectionsService
{
    private readonly IElectionRepository _electionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ElectionsService> _logger;

    public ElectionsService(IElectionRepository electionRepository, IMapper mapper, ILogger<ElectionsService> logger)
    {
        _electionRepository = electionRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Elections> CreateElectionAsync(ElectionsPostDTO electionDto)
    {
        try
        {
            var election = _mapper.Map<Elections>(electionDto);
            await _electionRepository.AddAsync(election);

            _logger.LogInformation("Election created successfully with ID {ElectionId}.", election.ElectionID);
            return election;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating an election: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<Elections> UpdateElectionAsync(ElectionsPutDTO electionDto)
    {
        try
        {
            var election = await _electionRepository.GetByIdAsync(electionDto.ElectionID);
            if (election == null)
            {
                _logger.LogWarning("Election with ID {ElectionId} not found.", electionDto.ElectionID);
                throw new KeyNotFoundException($"Election with ID {electionDto.ElectionID} not found.");
            }

            _mapper.Map(electionDto, election);
            await _electionRepository.UpdateAsync(election);

            _logger.LogInformation("Election with ID {ElectionId} updated successfully.", electionDto.ElectionID);
            return election;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the election with ID {ElectionId}: {ErrorMessage}", electionDto.ElectionID, ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteElectionAsync(int electionId)
    {
        try
        {
            var exists = await _electionRepository.ExistsAsync(electionId);
            if (!exists)
            {
                _logger.LogWarning("Election with ID {ElectionId} not found.", electionId);
                throw new KeyNotFoundException($"Election with ID {electionId} not found.");
            }

            await _electionRepository.DeleteAsync(electionId);

            _logger.LogInformation("Election with ID {ElectionId} deleted successfully.", electionId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the election with ID {ElectionId}: {ErrorMessage}", electionId, ex.Message);
            throw;
        }
    }

    public async Task<Elections> GetElectionByIdAsync(int electionId)
    {
        try
        {
            var election = await _electionRepository.GetByIdAsync(electionId);
            if (election == null)
            {
                _logger.LogWarning("Election with ID {ElectionId} not found.", electionId);
                throw new KeyNotFoundException($"Election with ID {electionId} not found.");
            }

            return election;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the election with ID {ElectionId}: {ErrorMessage}", electionId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Elections>> GetAllElectionsAsync()
    {
        try
        {
            var elections = await _electionRepository.GetAllAsync();
            return elections;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all elections: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
