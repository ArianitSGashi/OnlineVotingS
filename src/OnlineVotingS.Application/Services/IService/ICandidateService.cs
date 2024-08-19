using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.IService;

public interface ICandidateService
{
    Task<Candidates> CreateCandidateAsync(CandidatesPostDTO candidateDto);
    Task<Candidates> UpdateCandidateAsync(CandidatesPutDTO candidateDto);
    Task<bool> DeleteCandidateAsync(int candidateId);
    Task<Candidates> GetCandidateByIdAsync(int candidateId);
    Task<IEnumerable<Candidates>> GetAllCandidatesAsync();
    Task<IEnumerable<Candidates>> GetCandidatesByElectionIdAsync(int electionId);
}
