using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetAllComplaintCommand : IRequest<IEnumerable<Complaints>>
{
}