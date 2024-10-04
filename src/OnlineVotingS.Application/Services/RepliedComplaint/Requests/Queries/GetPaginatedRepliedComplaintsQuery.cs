using FluentResults;
using MediatR;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries
{
    public class GetPaginatedRepliedComplaintsQuery : IRequest<Result<PaginatedResult<RepliedComplaints>>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetPaginatedRepliedComplaintsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; }
    public int PageIndex { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedResult(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        Items = items;
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}

