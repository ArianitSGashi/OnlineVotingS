using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries
{
    public class GetPaginatedElectionsHandler : IRequestHandler<GetPaginatedElectionsQuery, Result<PaginatedResult<Elections>>>
    {
        private readonly IElectionRepository _electionRepository;
        private readonly ILogger<GetPaginatedElectionsHandler> _logger;

        public GetPaginatedElectionsHandler(IElectionRepository electionRepository, ILogger<GetPaginatedElectionsHandler> logger)
        {
            _electionRepository = electionRepository;
            _logger = logger;
        }

        public async Task<Result<PaginatedResult<Elections>>> Handle(GetPaginatedElectionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalCount = await _electionRepository.GetTotalElectionsCountAsync();
                var items = await _electionRepository.GetElectionsPaginatedAsync(request.PageNumber, request.PageSize);

                var paginatedResult = new PaginatedResult<Elections>(items, totalCount, request.PageNumber, request.PageSize);
                return FluentResults.Result.Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching paginated elections.");
                return FluentResults.Result.Fail<PaginatedResult<Elections>>("An error occurred while fetching paginated elections.");
            }
        }
    }
}
