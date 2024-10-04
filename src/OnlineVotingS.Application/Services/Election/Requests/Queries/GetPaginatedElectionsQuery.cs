using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries
{
    public class GetPaginatedElectionsQuery : IRequest<Result<PaginatedResult<Elections>>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetPaginatedElectionsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
