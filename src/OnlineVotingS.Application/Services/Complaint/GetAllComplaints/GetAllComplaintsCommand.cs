using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.GetAllComplaints;

public class GetAllComplaintsCommand : IRequest<IEnumerable<Complaints>>
{
}
