using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.IService;

public interface IElectionsService
    {
        Task<Elections> CreateElectionAsync(ElectionsPostDTO electionDto);
        Task<Elections> UpdateElectionAsync(ElectionsPutDTO electionDto);
        Task<bool> DeleteElectionAsync(int electionId);
        Task<Elections> GetElectionByIdAsync(int electionId);
        Task<IEnumerable<Elections>> GetAllElectionsAsync();
    }


