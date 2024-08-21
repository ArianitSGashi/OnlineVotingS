using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.ImplService;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.DeleteComplaint;

public class DeleteComplaintCommandHandler : IRequestHandler<DeleteComplaintCommand, bool>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public DeleteComplaintCommandHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _complaintRepository.ExistsAsync(request.ComplaintId);
            if (!exists)
            {
                _logger.LogWarning($"Complaint with ID {request.ComplaintId} not found.");
            }
            await _complaintRepository.DeleteAsync(request.ComplaintId);
            _logger.LogInformation($"Complaint with ID {request.ComplaintId} deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the complaint with ComplaintID : {complaintId}: {ex.Message}");
            throw;
        }
    }
}
