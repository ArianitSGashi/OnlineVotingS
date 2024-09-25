using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetByComplaintDateCommand : IRequest<Result<IEnumerable<Complaints>>>
{
    public DateTime Date { get;}

    public GetByComplaintDateCommand(DateTime date)
    {
        Date = date;
    }
}