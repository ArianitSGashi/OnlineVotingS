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

namespace OnlineVotingS.Application.Services.ImplService
{
    internal class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IMapper _mapper;

        public ComplaintService(IComplaintRepository complaintRepository, IMapper mapper)
        {
            _complaintRepository = complaintRepository;
            _mapper = mapper;
        }
        public async Task<Complaints> CreateComplaintsAsync(ComplaintsPostDTO complaintDto)
        {
            var complaint = _mapper.Map<Complaints>(complaintDto);
            await _complaintRepository.AddAsync(complaint);
            return complaint;
        }

        public async Task<bool> DeleteComplaintsAsync(int complaintId)
        {
            var exists = await _complaintRepository.ExistsAsync(complaintId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Complaint with ID {complaintId} not found.");
            }

            await _complaintRepository.DeleteAsync(complaintId);
            return true;
        }

        public async Task<IEnumerable<Complaints>> GetAllComplaintsAsync()
        {
            return await _complaintRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Complaints>> GetComplaintByElectionIdAsync(int electionId)
        {
            return await _complaintRepository.GetByElectionIdAsync(electionId);
        }

        public async Task<Complaints> GetComplaintsByIdAsync(int complaintId)
        {
            return await _complaintRepository.GetByIdAsync(complaintId);
        }

        public async Task<IEnumerable<Complaints>> GetComplaintsByUserIdAsync(string userId)
        {
            return await _complaintRepository.GetByUserIdAsync(userId);
        }

        public async Task<Complaints> UpdateComplaintsAsync(ComplaintsPutDTO complaintDto)
        {
            var complaint = await _complaintRepository.GetByIdAsync(complaintDto.ComplaintID);
            if (complaint == null)
            {
                throw new KeyNotFoundException($"Complaint with ID {complaintDto.ComplaintID} not found.");
            }

            _mapper.Map(complaintDto, complaint);
            await _complaintRepository.UpdateAsync(complaint);
            return complaint;
        }
    }
}
