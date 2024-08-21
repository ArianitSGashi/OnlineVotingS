using MediatR;

namespace OnlineVotingS.Application.Services.Complaint.DeleteComplaint;

public class DeleteComplaintCommand : IRequest<bool>
{
    public int ComplaintId { get; set; }
}
