using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetAllComplaintCommand : IRequest<Result<IEnumerable<Complaints>>>
{
}