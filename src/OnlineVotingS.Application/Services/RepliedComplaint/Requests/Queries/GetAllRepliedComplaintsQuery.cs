using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetAllRepliedComplaintsQuery : IRequest<IEnumerable<RepliedComplaints>>
{
}