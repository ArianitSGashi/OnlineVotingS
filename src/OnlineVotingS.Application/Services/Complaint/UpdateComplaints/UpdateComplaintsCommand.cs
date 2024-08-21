using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.UpdateComplaints;

public class UpdateComplaintsCommand : IRequest<Complaints>
{
    public int ComplaintID { get; set; }
    public int UserID { get; set; }
    public int ElectionID { get; set; }
    public string ComplaintText { get; set; } = null!;
    public DateTime ComplaintDate { get; set; }
}
