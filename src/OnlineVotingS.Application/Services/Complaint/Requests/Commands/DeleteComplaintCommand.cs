using FluentResults;
using MediatR;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Commands;

public class DeleteComplaintCommand : IRequest<Result<bool>>
{
    public int ComplaintId { get; }

    public DeleteComplaintCommand(int complaintId)
    {
        ComplaintId = complaintId;
    }
}
