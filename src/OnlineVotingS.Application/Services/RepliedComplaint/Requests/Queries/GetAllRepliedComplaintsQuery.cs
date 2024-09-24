using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetAllRepliedComplaintsQuery : IRequest<Result<IEnumerable<RepliedComplaints>>>
{
}