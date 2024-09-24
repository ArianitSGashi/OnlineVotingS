using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetRepliedComplaintByIdQueryHandler : IRequestHandler<GetRepliedComplaintByIdQuery, Result<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetRepliedComplaintByIdQueryHandler> _logger;

    public GetRepliedComplaintByIdQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRepliedComplaintByIdQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<Result<RepliedComplaints>> Handle(GetRepliedComplaintByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedcomplaint = await _repliedComplaintsRepository.GetByIdAsync(request.RepliedComplaintId);
            if (repliedcomplaint == null)
            {
                return new Result<RepliedComplaints>().WithError(ErrorCodes.REPLIED_COMPLAINT_NOT_FOUND.ToString());
            }
            return Ok(repliedcomplaint);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaintId, ex.Message);
            return new Result<RepliedComplaints>().WithError(ErrorCodes.REPLIED_COMPLAINT_NOT_FOUND.ToString());
        }
    }
}