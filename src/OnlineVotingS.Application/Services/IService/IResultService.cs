using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.IService;

public interface IResultService
{
    Task<Result> CreateResultAsync(ResultPostDTO resultDto);
    Task<Result> UpdateResultAsync(ResultPutDTO resultDto);
    Task<bool> DeleteResultAsync(int resultId);
    Task<Result> GetResultByIdAsync(int resultId);
    Task<IEnumerable<Result>> GetAllResultsAsync();
    Task<IEnumerable<Result>> GetResultsByElectionIdAsync(int electionId);
    Task<IEnumerable<Result>> GetResultsByCandidateIdAsync(int candidateId);
    Task<IEnumerable<Result>> GetResultsByTotalVotesGreaterThanAsync(int votes);
}