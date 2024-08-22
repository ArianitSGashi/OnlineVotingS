using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Application.Services.ImplService;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class DeleteComplaintHandler : IRequestHandler<DeleteCampaignCommand, bool>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public DeleteComplaintHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _complaintRepository.ExistsAsync(request.CampaignId);
            if (!exists)
            {
                _logger.LogWarning($"Complaint with ID {request.CampaignId} not found.");
            }
            await _complaintRepository.DeleteAsync(request.CampaignId);
            _logger.LogInformation($"Complaint with ID {request.CampaignId} deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the complaint with ComplaintID : {request.CampaignId}: {ex.Message}");
            throw;
        }
    }
}
