using static FluentResults.Result;
using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Commands;

public class UpdateRepliedComplaintCommandHandler : IRequestHandler<UpdateRepliedComplaintCommand, Result<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateRepliedComplaintCommandHandler> _logger;

    public UpdateRepliedComplaintCommandHandler(IRepliedComplaintsRepository repliedComplaintsRepository, IMapper mapper, ILogger<UpdateRepliedComplaintCommandHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<RepliedComplaints>> Handle(UpdateRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaint = await _repliedComplaintsRepository.GetByIdAsync(request.RepliedComplaint.RepliedComplaintID);
            if (repliedComplaint == null)
            {
                var errorMessage = $"Replied complaint with ID {request.RepliedComplaint.RepliedComplaintID} not found.";
                _logger.LogWarning(errorMessage);
                return new Result<RepliedComplaints>().WithError(errorMessage); return Fail(ErrorCodes.REPLIED_COMPLAINT_NOT_FOUND.ToString());
            }

            _mapper.Map(request.RepliedComplaint, repliedComplaint);
            await _repliedComplaintsRepository.UpdateAsync(repliedComplaint);
            return Ok(repliedComplaint);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaint.RepliedComplaintID, ex.Message);
            return new Result<RepliedComplaints>().WithError(ErrorCodes.FEEDBACK_UPDATE_FAILED.ToString());
        }
    }
}