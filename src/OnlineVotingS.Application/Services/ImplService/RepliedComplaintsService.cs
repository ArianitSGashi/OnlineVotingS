using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

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

    public async Task<RepliedComplaints> CreateRepliedComplaintAsync(RepliedComplaintsPostDTO repliedComplaintDto)
    {
        try
        {
            var repliedComplaint = _mapper.Map<RepliedComplaints>(repliedComplaintDto);
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

    public async Task<RepliedComplaints> UpdateRepliedComplaintAsync(RepliedComplaintsPutDTO repliedComplaintDto)
    {
        try
        {
            var repliedComplaint = await _repliedComplaintsRepository.GetByIdAsync(repliedComplaintDto.RepliedComplaintID);
            if (repliedComplaint == null)
            {
                _logger.LogWarning("Replied complaint with ID {RepliedComplaintId} not found.", repliedComplaintDto.RepliedComplaintID);
                throw new KeyNotFoundException($"Replied complaint with ID {repliedComplaintDto.RepliedComplaintID} not found.");
            }

            _mapper.Map(repliedComplaintDto, repliedComplaint);
            await _repliedComplaintsRepository.UpdateAsync(repliedComplaint);

            _logger.LogInformation("Replied complaint with ID {RepliedComplaintId} updated successfully.", repliedComplaintDto.RepliedComplaintID);
            return repliedComplaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", repliedComplaintDto.RepliedComplaintID, ex.Message);
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

    public async Task<IEnumerable<RepliedComplaints>> GetRepliedComplaintsByComplaintIDAsync(int complaintID)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintsRepository.GetByComplaintIDAsync(complaintID);
            return repliedComplaints;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching replied complaints for complaint ID {ComplaintID}: {ErrorMessage}", complaintID, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<RepliedComplaints>> GetRepliedComplaintsByReplyTextAsync(string replyText)
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

    public async Task<IEnumerable<RepliedComplaints>> GetRecentRepliedComplaintsAsync(DateTime date)
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