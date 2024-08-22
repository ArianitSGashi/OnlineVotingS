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

public class GetAllRepliedComplaintsQueryHandler : IRequestHandler<GetAllRepliedComplaintsQuery, IEnumerable<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly ILogger<GetAllRepliedComplaintsQueryHandler> _logger;

    public GetAllRepliedComplaintsQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetAllRepliedComplaintsQueryHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RepliedComplaints>> Handle(GetAllRepliedComplaintsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _repliedComplaintsRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving all replied complaints: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}