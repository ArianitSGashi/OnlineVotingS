using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

public class GetRepliedComplaintsByReplyTextQueryHandler : IRequestHandler<GetRepliedComplaintsByReplyTextQuery, IEnumerable<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetRepliedComplaintsByReplyTextQueryHandler> _logger;

    public GetRepliedComplaintsByReplyTextQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRepliedComplaintsByReplyTextQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RepliedComplaints>> Handle(GetRepliedComplaintsByReplyTextQuery request, CancellationToken cancellationToken)
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