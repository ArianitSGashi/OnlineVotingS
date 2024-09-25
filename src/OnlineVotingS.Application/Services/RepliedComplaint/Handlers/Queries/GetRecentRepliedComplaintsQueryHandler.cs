using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetRecentRepliedComplaintsQueryHandler : IRequestHandler<GetRecentRepliedComplaintsQuery, Result<IEnumerable<RepliedComplaints>>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetRecentRepliedComplaintsQueryHandler> _logger;

    public GetRecentRepliedComplaintsQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRecentRepliedComplaintsQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<RepliedComplaints>>> Handle(GetRecentRepliedComplaintsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var recentReplies = await _repliedComplaintsRepository.GetRecentRepliesAsync(request.Date);
            return Ok(recentReplies);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving recent replied complaints from {Date}: {ErrorMessage}", request.Date, ex.Message);
            return new Result<IEnumerable<RepliedComplaints>>().WithError(ErrorCodes.REPLIED_COMPLAINT_NOT_FOUND.ToString());
        }
    }
}