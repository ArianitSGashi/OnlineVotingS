using static FluentResults.Result;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Domain.Errors;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Commands;

public class DeleteRepliedComplaintCommandHandler : IRequestHandler<DeleteRepliedComplaintCommand, Result<bool>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<DeleteRepliedComplaintCommandHandler> _logger;

    public DeleteRepliedComplaintCommandHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<DeleteRepliedComplaintCommandHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _repliedComplaintsRepository.ExistsAsync(request.RepliedComplaintId);
            if (!exists)
            {
                var errorMessage = $"Replied complaint with ID {request.RepliedComplaintId} not found.";
                return new Result<bool>().WithError(errorMessage);
            }

            await _repliedComplaintsRepository.DeleteAsync(request.RepliedComplaintId);
            return Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the replied complaint with ID {RepliedComplaintId}", request.RepliedComplaintId);
            return new Result<bool>().WithError(ErrorCodes.REPLIED_COMPLAINT_DELETION_FAILED.ToString());
        }
    }
}
