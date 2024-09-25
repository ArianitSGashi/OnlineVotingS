using FluentResults;
using static FluentResults.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class DeleteComplaintHandler : IRequestHandler<DeleteComplaintCommand, Result<bool>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<DeleteComplaintHandler> _logger;

    public DeleteComplaintHandler(IComplaintRepository complaintRepository, ILogger<DeleteComplaintHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteComplaintCommand request, CancellationToken cancellationToken)
    {
        var exists = await _complaintRepository.ExistsAsync(request.ComplaintId);
        if (!exists)
        {
            return new Result<bool>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
        }

        try
        {
            await _complaintRepository.DeleteAsync(request.ComplaintId);
            return Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting complaint with ComplaintID: {ComplaintId}: {ErrorMessage}", request.ComplaintId, ex.Message);
            return new Result<bool>().WithError(ErrorCodes.COMPLAIN_DELETION_FAILED.ToString());
        }
    }
}
