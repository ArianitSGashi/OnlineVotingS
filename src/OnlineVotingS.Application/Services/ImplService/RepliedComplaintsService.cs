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

public class RepliedComplaintsService : IRepliedComplaintsService
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RepliedComplaintsService> _logger;

    public RepliedComplaintsService(IRepliedComplaintsRepository repliedComplaintsRepository, IMapper mapper, ILogger<RepliedComplaintsService> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<RepliedComplaints> CreateRepliedComplaintAsync(RepliedComplaintsPostDTO repliedComplaintsDto)
    {
        try
        {
            var repliedComplaint = _mapper.Map<RepliedComplaints>(repliedComplaintsDto);
            await _repliedComplaintsRepository.AddAsync(repliedComplaint);
            _logger.LogInformation("Replied complaint created successfully with ID {RepliedComplaintId}.", repliedComplaint.RepliedComplaintID);
            return repliedComplaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a replied complaint: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<RepliedComplaints> UpdateRepliedComplaintAsync(RepliedComplaintsPutDTO repliedComplaintsDto)
    {
        try
        {
            var repliedComplaint = await _repliedComplaintsRepository.GetByIdAsync(repliedComplaintsDto.RepliedComplaintID);
            if (repliedComplaint == null)
            {
                _logger.LogWarning("Replied complaint with ID {RepliedComplaintId} not found.", repliedComplaintsDto.RepliedComplaintID);
                throw new KeyNotFoundException($"Replied complaint with ID {repliedComplaintsDto.RepliedComplaintID} not found.");
            }

            _mapper.Map(repliedComplaintsDto, repliedComplaint);
            await _repliedComplaintsRepository.UpdateAsync(repliedComplaint);
            _logger.LogInformation("Replied complaint with ID {RepliedComplaintId} updated successfully.", repliedComplaintsDto.RepliedComplaintID);
            return repliedComplaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", repliedComplaintsDto.RepliedComplaintID, ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteRepliedComplaintAsync(int repliedComplaintId)
    {
        try
        {
            var exists = await _repliedComplaintsRepository.ExistsAsync(repliedComplaintId);
            if (!exists)
            {
                _logger.LogWarning("Replied complaint with ID {RepliedComplaintId} not found.", repliedComplaintId);
                throw new KeyNotFoundException($"Replied complaint with ID {repliedComplaintId} not found.");
            }

            await _repliedComplaintsRepository.DeleteAsync(repliedComplaintId);
            _logger.LogInformation("Replied complaint with ID {RepliedComplaintId} deleted successfully.", repliedComplaintId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", repliedComplaintId, ex.Message);
            throw;
        }
    }

    public async Task<RepliedComplaints> GetRepliedComplaintByIdAsync(int repliedComplaintId)
    {
        try
        {
            var repliedComplaint = await _repliedComplaintsRepository.GetByIdAsync(repliedComplaintId);
            if (repliedComplaint == null)
            {
                _logger.LogWarning("Replied complaint with ID {RepliedComplaintId} not found.", repliedComplaintId);
                throw new KeyNotFoundException($"Replied complaint with ID {repliedComplaintId} not found.");
            }

            return repliedComplaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", repliedComplaintId, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<RepliedComplaints>> GetAllRepliedComplaintsAsync()
    {
        try
        {
            var repliedComplaints = await _repliedComplaintsRepository.GetAllAsync();
            return repliedComplaints;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all replied complaints: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<RepliedComplaints>> GetByComplaintIDAsync(int complaintID)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintsRepository.GetByComplaintIDAsync(complaintID);
            return repliedComplaints;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching replied complaints for complaint ID {ComplaintId}: {ErrorMessage}", complaintID, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<RepliedComplaints>> GetByReplyTextAsync(string replyText)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintsRepository.GetByReplyTextAsync(replyText);
            return repliedComplaints;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching replied complaints with reply text {ReplyText}: {ErrorMessage}", replyText, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<RepliedComplaints>> GetRecentRepliesAsync(DateTime date)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintsRepository.GetRecentRepliesAsync(date);
            return repliedComplaints;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching recent replied complaints: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}