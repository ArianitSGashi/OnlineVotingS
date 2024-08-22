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

public class GetByReplyTextQueryHandler : IRequestHandler<GetByReplyTextQuery, IEnumerable<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetByReplyTextQueryHandler> _logger;

    public GetByReplyTextQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetByReplyTextQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RepliedComplaints>> Handle(GetByReplyTextQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repliedComplaintsRepository.GetByReplyTextAsync(request.ReplyText);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving replied complaints by reply text '{ReplyText}': {ErrorMessage}", request.ReplyText, ex.Message);
            throw;
        }
    }
}