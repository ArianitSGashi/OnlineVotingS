using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaints.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Handlers.Commands;

public class DeleteRepliedComplaintHandler : IRequestHandler<DeleteRepliedComplaintCommand, bool>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintRepository;
    private readonly ILogger<DeleteRepliedComplaintHandler> _logger;

    public DeleteRepliedComplaintHandler(IRepliedComplaintsRepository repliedComplaintRepository, ILogger<DeleteRepliedComplaintHandler> logger)
    {
        _repliedComplaintRepository = repliedComplaintRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _repliedComplaintRepository.ExistsAsync(request.RepliedComplaintId);
            if (!exists)
            {
                _logger.LogWarning("Replied complaint with ID {RepliedComplaintId} not found.", request.RepliedComplaintId);
                throw new KeyNotFoundException($"Replied complaint with ID {request.RepliedComplaintId} not found.");
            }

            await _repliedComplaintRepository.DeleteAsync(request.RepliedComplaintId);

            _logger.LogInformation("Replied complaint with ID {RepliedComplaintId} deleted successfully.", request.RepliedComplaintId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaintId, ex.Message);
            throw;
        }
    }
}