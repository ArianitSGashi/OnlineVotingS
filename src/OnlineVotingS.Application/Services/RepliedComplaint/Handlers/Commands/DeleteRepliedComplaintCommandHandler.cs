using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Commands;

public class DeleteRepliedComplaintCommandHandler : IRequestHandler<DeleteRepliedComplaintCommand, FluentResults.Result>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<DeleteRepliedComplaintCommandHandler> _logger;

    public DeleteRepliedComplaintCommandHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<DeleteRepliedComplaintCommandHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<FluentResults.Result> Handle(DeleteRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _repliedComplaintsRepository.ExistsAsync(request.RepliedComplaintId);
            if (!exists)
            {
                return FluentResults.Result.Fail($"Replied complaint with ID {request.RepliedComplaintId} not found.");
            }

            await _repliedComplaintsRepository.DeleteAsync(request.RepliedComplaintId);
            return FluentResults.Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaintId, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex)); 
        }
    }
}
