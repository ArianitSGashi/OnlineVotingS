using MediatR;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Commands;

public class DeleteComplaintCommand : IRequest<bool>
{
    public int ComplaintId{ get; set; }

    public DeleteComplaintCommand(int complaintId)
    {
        ComplaintId = complaintId;
    }
}
