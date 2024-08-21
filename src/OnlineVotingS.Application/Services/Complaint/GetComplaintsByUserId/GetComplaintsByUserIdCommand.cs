using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.GetComplaintsByUserId;

public class GetComplaintsByUserIdCommand : IRequest<IEnumerable<Complaints>>
{
    public string UserId{ get; set; }
}
