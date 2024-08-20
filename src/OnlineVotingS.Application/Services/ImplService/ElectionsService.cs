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

public class ElectionsService : IElectionsService
    {
        private readonly IElectionRepository _electionRepository;
        private readonly IMapper _mapper;

        public ElectionsService(IElectionRepository electionRepository, IMapper mapper)
        {
            _electionRepository = electionRepository;
            _mapper = mapper;
        }

        public async Task<Elections> CreateElectionAsync(ElectionsPostDTO electionDto)
        {
            var election = _mapper.Map<Elections>(electionDto);
            await _electionRepository.AddAsync(election);
            return election;
        }

        public async Task<Elections> UpdateElectionAsync(ElectionsPutDTO electionDto)
        {
            var election = await _electionRepository.GetByIdAsync(electionDto.ElectionID);
            if (election == null)
            {
                throw new KeyNotFoundException($"Election with ID {electionDto.ElectionID} not found.");
            }

            _mapper.Map(electionDto, election);
            await _electionRepository.UpdateAsync(election);
            return election;
        }

        public async Task<bool> DeleteElectionAsync(int electionId)
        {
            var exists = await _electionRepository.ExistsAsync(electionId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Election with ID {electionId} not found.");
            }

            await _electionRepository.DeleteAsync(electionId);
            return true;
        }

        public async Task<Elections> GetElectionByIdAsync(int electionId)
        {
            return await _electionRepository.GetByIdAsync(electionId);
        }

        public async Task<IEnumerable<Elections>> GetAllElectionsAsync()
        {
            return await _electionRepository.GetAllAsync();
        }
    }

