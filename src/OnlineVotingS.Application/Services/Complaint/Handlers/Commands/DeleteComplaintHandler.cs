using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.CostumExceptions;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class DeleteComplaintHandler : IRequestHandler<DeleteComplaintCommand, FluentResults.Result<bool>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<DeleteComplaintHandler> _logger;

    public DeleteComplaintHandler(IComplaintRepository complaintRepository, ILogger<DeleteComplaintHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<FluentResults.Result<bool>> Handle(DeleteComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _complaintRepository.ExistsAsync(request.ComplaintId);
            if (!exists)
            {
                var errorMessage = $"Complaint with ID: {request.ComplaintId} not found.";
                return FluentResults.Result.Fail(new ExceptionalError(new KeyNotFoundException(errorMessage)));
            }

            await _complaintRepository.DeleteAsync(request.ComplaintId);
            return FluentResults.Result.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting complaint with ComplaintID: {ComplaintId}: {ErrorMessage}", request.ComplaintId, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
