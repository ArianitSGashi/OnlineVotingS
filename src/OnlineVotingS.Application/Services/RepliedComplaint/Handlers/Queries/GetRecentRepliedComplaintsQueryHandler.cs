using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetRecentRepliedComplaintsQueryHandler : IRequestHandler<GetRecentRepliedComplaintsQuery, IEnumerable<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetRecentRepliedComplaintsQueryHandler> _logger;

    public GetRecentRepliedComplaintsQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRecentRepliedComplaintsQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RepliedComplaints>> Handle(GetRecentRepliedComplaintsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repliedComplaintsRepository.GetRecentRepliesAsync(request.Date);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving recent replied complaints from {Date}: {ErrorMessage}", request.Date, ex.Message);
            throw;
        }
    }
}