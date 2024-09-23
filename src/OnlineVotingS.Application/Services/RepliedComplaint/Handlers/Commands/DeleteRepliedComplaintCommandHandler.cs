using static FluentResults.Result;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Domain.Errors;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Commands;

public class DeleteRepliedComplaintCommandHandler : IRequestHandler<DeleteRepliedComplaintCommand, Result>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<DeleteRepliedComplaintCommandHandler> _logger;

    public DeleteRepliedComplaintCommandHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<DeleteRepliedComplaintCommandHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _repliedComplaintsRepository.ExistsAsync(request.RepliedComplaintId);
            if (!exists)
            {
                var errorMessage = $"Replied complaint with ID {request.RepliedComplaintId} not found.";
                _logger.LogWarning(errorMessage);
                return new Result().WithError(errorMessage);
            }

            await _repliedComplaintsRepository.DeleteAsync(request.RepliedComplaintId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the replied complaint with ID {RepliedComplaintId}", request.RepliedComplaintId);
            return new Result().WithError(ErrorCodes.FEEDBACK_DELETION_FAILED.ToString());
        }
    }
}
