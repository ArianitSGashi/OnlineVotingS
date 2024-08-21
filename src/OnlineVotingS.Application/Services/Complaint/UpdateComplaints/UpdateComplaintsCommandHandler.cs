using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.ImplService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.UpdateComplaints;

public class UpdateComplaintsCommandHandler : IRequestHandler<UpdateComplaintsCommand, Complaints>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public UpdateComplaintsCommandHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Complaints> Handle(UpdateComplaintsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaint = await _complaintRepository.GetByIdAsync(request.ComplaintID);
            if (complaint == null)
            {
                _logger.LogWarning($"Complaint with ID {request.ComplaintID} not found.");
            }
            _mapper.Map(request, complaint);
            await _complaintRepository.UpdateAsync(complaint);
            _logger.LogInformation($"Complaint with ID {request.ComplaintID} updated successfully.");
            return complaint;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating the complaint with ComplaintID: {request.ComplaintID}: {ex.Message}");
            throw;
        }
    }
}