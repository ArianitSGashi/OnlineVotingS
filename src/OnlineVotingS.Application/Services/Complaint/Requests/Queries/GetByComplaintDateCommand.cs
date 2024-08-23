using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetByComplaintDateCommand : IRequest<IEnumerable<Complaints>>
{
    public DateTime Date { get;}

    public GetByComplaintDateCommand(DateTime date)
    {
        Date = date;
    }
}