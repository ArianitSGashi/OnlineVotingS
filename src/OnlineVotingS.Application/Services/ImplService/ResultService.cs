using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.ImplService;

public class ResultService : IResultService
{
    private readonly IResultRepository _resultRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ResultService> _logger;

    public ResultService(IResultRepository resultRepository, IMapper mapper, ILogger<ResultService> logger)
    {
        _resultRepository = resultRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result> CreateResultAsync(ResultPostDTO resultDto)
    {
        try
        {
            var result = _mapper.Map<Result>(resultDto);
            await _resultRepository.AddAsync(result);

            _logger.LogInformation("Result created successfully with ID {ResultId}.", result.ResultID);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a result: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<Result> UpdateResultAsync(ResultPutDTO resultDto)
    {
        try
        {
            var result = await _resultRepository.GetByIdAsync(resultDto.ResultID);
            if (result == null)
            {
                _logger.LogWarning("Result with ID {ResultId} not found.", resultDto.ResultID);
                throw new KeyNotFoundException($"Result with ID {resultDto.ResultID} not found.");
            }

            _mapper.Map(resultDto, result);
            await _resultRepository.UpdateAsync(result);

            _logger.LogInformation("Result with ID {ResultId} updated successfully.", resultDto.ResultID);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the result with ID {ResultId}: {ErrorMessage}", resultDto.ResultID, ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteResultAsync(int resultId)
    {
        try
        {
            var exists = await _resultRepository.ExistsAsync(resultId);
            if (!exists)
            {
                _logger.LogWarning("Result with ID {ResultId} not found.", resultId);
                throw new KeyNotFoundException($"Result with ID {resultId} not found.");
            }

            await _resultRepository.DeleteAsync(resultId);

            _logger.LogInformation("Result with ID {ResultId} deleted successfully.", resultId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the result with ID {ResultId}: {ErrorMessage}", resultId, ex.Message);
            throw;
        }
    }

    public async Task<Result> GetResultByIdAsync(int resultId)
    {
        try
        {
            var result = await _resultRepository.GetByIdAsync(resultId);
            if (result == null)
            {
                _logger.LogWarning("Result with ID {ResultId} not found.", resultId);
                throw new KeyNotFoundException($"Result with ID {resultId} not found.");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the result with ID {ResultId}: {ErrorMessage}", resultId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Result>> GetAllResultsAsync()
    {
        try
        {
            var results = await _resultRepository.GetAllAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all results: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Result>> GetResultsByElectionIdAsync(int electionId)
    {
        try
        {
            var results = await _resultRepository.GetByElectionIdAsync(electionId);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching results for election ID {ElectionId}: {ErrorMessage}", electionId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Result>> GetResultsByCandidateIdAsync(int candidateId)
    {
        try
        {
            var results = await _resultRepository.GetByCandidateIdAsync(candidateId);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching results for candidate ID {CandidateId}: {ErrorMessage}", candidateId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Result>> GetResultsByTotalVotesGreaterThanAsync(int votes)
    {
        try
        {
            var results = await _resultRepository.GetByTotalVotesGreaterThanAsync(votes);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching results with total votes greater than {Votes}: {ErrorMessage}", votes, ex.Message);
            throw;
        }
    }
}
