using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.IService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.ImplService;

public class ComplaintService : IComplaintService
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public ComplaintService(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Complaints> CreateComplaintsAsync(ComplaintsPostDTO complaintDto)
    {
        try
        {
            var complaint = _mapper.Map<Complaints>(complaintDto);
            await _complaintRepository.AddAsync(complaint);
            _logger.LogInformation($"Complaint added successfully.");
            return complaint;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while creating the complaint: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteComplaintsAsync(int complaintId)
    {
        try
        {
            var exists = await _complaintRepository.ExistsAsync(complaintId);
            if (!exists)
            {
                _logger.LogWarning($"Complaint with ID {complaintId} not found.");
            }

            await _complaintRepository.DeleteAsync(complaintId);
            _logger.LogInformation($"Complaint with ID {complaintId} deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the complaint with ComplaintID : {complaintId}: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Complaints>> GetAllComplaintsAsync()
    {
        try
        {
            return await _complaintRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching the complaints: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Complaints>> GetComplaintByElectionIdAsync(int electionId)
    {
        try
        {
            return await _complaintRepository.GetByElectionIdAsync(electionId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching the complaints with ElectionID {electionId}: {ex.Message}");
            throw;
        }
    }

    public async Task<Complaints> GetComplaintsByIdAsync(int complaintId)
    {
        try
        {
            return await _complaintRepository.GetByIdAsync(complaintId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching the complaint with ComplaintID: {complaintId}: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Complaints>> GetComplaintsByUserIdAsync(string userId)
    {
        try
        {
            return await _complaintRepository.GetByUserIdAsync(userId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching the complaints with UserID {userId}: {ex.Message}");
            throw;
        }
    }

    public async Task<Complaints> UpdateComplaintsAsync(ComplaintsPutDTO complaintDto)
    {
        try
        {
            var complaint = await _complaintRepository.GetByIdAsync(complaintDto.ComplaintID);
            if (complaint == null)
            {
                _logger.LogWarning($"Complaint with ID {complaintDto.ComplaintID} not found.");
            }

            _mapper.Map(complaintDto, complaint);
            await _complaintRepository.UpdateAsync(complaint);
            _logger.LogInformation($"Complaint with ID {complaintDto.ComplaintID} updated successfully.");
            return complaint;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating the complaint with ComplaintID: {complaintDto.ComplaintID}: {ex.Message}");
            throw;
        }
    }
}