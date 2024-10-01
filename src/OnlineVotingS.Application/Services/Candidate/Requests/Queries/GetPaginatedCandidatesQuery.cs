using MediatR;
using OnlineVotingS.Application;
using OnlineVotingS.Domain.Entities;

public class GetPaginatedCandidatesQuery : IRequest<PaginatedList<Candidates>>
{
    public int PageNumber { get; }
    public int PageSize { get; }

    public GetPaginatedCandidatesQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
