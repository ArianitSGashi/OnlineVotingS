using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.CreateComplaint;

public class CreateComplaintCommand : IRequest<Complaints>
{
    public int UserID { get; set; }
    public int ElectionID { get; set; }
    public string ComplaintText { get; set; } = null!;
    public DateTime ComplaintDate { get; set; }
}
