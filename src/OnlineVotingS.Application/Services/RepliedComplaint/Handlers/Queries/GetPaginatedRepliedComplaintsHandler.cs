using FluentResults;
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

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries
{
    public class GetPaginatedRepliedComplaintsHandler : IRequestHandler<GetPaginatedRepliedComplaintsQuery, Result<PaginatedResult<RepliedComplaints>>>
    {
        private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
        private readonly ILogger<GetPaginatedRepliedComplaintsHandler> _logger;

        public GetPaginatedRepliedComplaintsHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetPaginatedRepliedComplaintsHandler> logger)
        {
            _repliedComplaintsRepository = repliedComplaintsRepository;
            _logger = logger;
        }

        public async Task<Result<PaginatedResult<RepliedComplaints>>> Handle(GetPaginatedRepliedComplaintsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalCount = await _repliedComplaintsRepository.GetTotalRepliedComplaintsCountAsync();
                var items = await _repliedComplaintsRepository.GetRepliedComplaintsPaginatedAsync(request.PageNumber, request.PageSize);

                var paginatedResult = new PaginatedResult<RepliedComplaints>(items, totalCount, request.PageNumber, request.PageSize);
                return FluentResults.Result.Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching paginated replied complaints.");
                return FluentResults.Result.Fail<PaginatedResult<RepliedComplaints>>("An error occurred while fetching paginated replied complaints.");
            }
        }
    }
}

