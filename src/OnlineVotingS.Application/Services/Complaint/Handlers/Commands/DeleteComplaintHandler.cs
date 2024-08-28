using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class DeleteComplaintHandler : IRequestHandler<DeleteComplaintCommand, bool>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<DeleteComplaintHandler> _logger;

    public DeleteComplaintHandler(IComplaintRepository complaintRepository, ILogger<DeleteComplaintHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _complaintRepository.ExistsAsync(request.ComplaintId);
            if (!exists)
            {
                throw new KeyNotFoundException($"Complaint with ID : {request.ComplaintId} not found.");
            }

            await _complaintRepository.DeleteAsync(request.ComplaintId);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while delete of complaint with ComplaintID: {ComplaintId}: {ErrorMessage}", request.ComplaintId, ex.Message);
            throw;
        }
    }
}