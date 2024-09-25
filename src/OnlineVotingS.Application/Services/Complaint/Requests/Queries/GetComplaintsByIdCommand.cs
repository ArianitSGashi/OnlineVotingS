using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetComplaintsByIdCommand : IRequest<Result<Complaints>>
{
    public int ComplaintId { get;}

    public GetComplaintsByIdCommand(int complaintId)
    {
        ComplaintId = complaintId;
    }
}