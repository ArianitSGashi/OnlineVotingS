using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetRecentRepliesQueryHandler : IRequestHandler<GetRecentRepliesQuery, IEnumerable<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetRecentRepliesQueryHandler> _logger;

    public GetRecentRepliesQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRecentRepliesQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RepliedComplaints>> Handle(GetRecentRepliesQuery request, CancellationToken cancellationToken)
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