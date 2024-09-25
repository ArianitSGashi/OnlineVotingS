using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetRecentRepliedComplaintsQuery : IRequest<Result<IEnumerable<RepliedComplaints>>>
{
    public DateTime Date { get;}

    public GetRecentRepliedComplaintsQuery(DateTime date)
    {
        Date = date;
    }
}