using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetAllRepliedComplaintsQueryHandler : IRequestHandler<GetAllRepliedComplaintsQuery, Result<IEnumerable<RepliedComplaints>>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetAllRepliedComplaintsQueryHandler> _logger;

    public GetAllRepliedComplaintsQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetAllRepliedComplaintsQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<RepliedComplaints>>> Handle(GetAllRepliedComplaintsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaints = await _repliedComplaintsRepository.GetAllAsync();
            return Ok(repliedComplaints);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving all replied complaints: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<RepliedComplaints>>().WithError(ErrorCodes.REPLIED_COMPLAINT_NOT_FOUND.ToString());
        }
    }
}