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

    public class RepliedComplaintsService : IRepliedComplaintsService
    {
        private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
        private readonly IMapper _mapper;

        public RepliedComplaintsService(IRepliedComplaintsRepository repliedComplaintsRepository, IMapper mapper)
        {
            _repliedComplaintsRepository = repliedComplaintsRepository;
            _mapper = mapper;
        }

        public async Task<RepliedComplaints> CreateRepliedComplaintAsync(RepliedComplaintsPostDTO repliedComplaintsDto)
        {
            var repliedComplaint = _mapper.Map<RepliedComplaints>(repliedComplaintsDto);
            await _repliedComplaintsRepository.AddAsync(repliedComplaint);
            return repliedComplaint;
        }

        public async Task<RepliedComplaints> UpdateRepliedComplaintAsync(RepliedComplaintsPutDTO repliedComplaintsDto)
        {
            var repliedComplaint = await _repliedComplaintsRepository.GetByIdAsync(repliedComplaintsDto.RepliedComplaintID);
            if (repliedComplaint == null)
            {
                throw new KeyNotFoundException($"Replied complaint with ID {repliedComplaintsDto.RepliedComplaintID} not found.");
            }

            _mapper.Map(repliedComplaintsDto, repliedComplaint);
            await _repliedComplaintsRepository.UpdateAsync(repliedComplaint);
            return repliedComplaint;
        }

        public async Task<bool> DeleteRepliedComplaintAsync(int repliedComplaintId)
        {
            var exists = await _repliedComplaintsRepository.ExistsAsync(repliedComplaintId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Replied complaint with ID {repliedComplaintId} not found.");
            }

            await _repliedComplaintsRepository.DeleteAsync(repliedComplaintId);
            return true;
        }

        public async Task<RepliedComplaints> GetRepliedComplaintByIdAsync(int repliedComplaintId)
        {
            return await _repliedComplaintsRepository.GetByIdAsync(repliedComplaintId);
        }

        public async Task<IEnumerable<RepliedComplaints>> GetAllRepliedComplaintsAsync()
        {
            return await _repliedComplaintsRepository.GetAllAsync();
        }

        public async Task<IEnumerable<RepliedComplaints>> GetByComplaintIDAsync(int complaintID)
        {
            return await _repliedComplaintsRepository.GetByComplaintIDAsync(complaintID);
        }

        public async Task<IEnumerable<RepliedComplaints>> GetByReplyTextAsync(string replyText)
        {
            return await _repliedComplaintsRepository.GetByReplyTextAsync(replyText);
        }

        public async Task<IEnumerable<RepliedComplaints>> GetRecentRepliesAsync(DateTime date)
        {
            return await _repliedComplaintsRepository.GetRecentRepliesAsync(date);
        }
    }