using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetComplaintsByUserIdCommand : IRequest<Result<IEnumerable<Complaints>>>
{
    public string UserId { get;}

    public GetComplaintsByUserIdCommand(string userId)
    {
        UserId = userId;
    }
}