using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetRecentRepliedComplaintsQuery : IRequest<IEnumerable<RepliedComplaints>>
{
    public DateTime Date { get;}

    public GetRecentRepliedComplaintsQuery(DateTime date)
    {
        Date = date;
    }
}