using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetComplaintsByUserIdCommand : IRequest<IEnumerable<Complaints>>
{
    public string UserId { get;}

    public GetComplaintsByUserIdCommand(string userId)
    {
        UserId = userId;
    }
}