using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Commands;

public class UpdateComplaintCommand : IRequest<Complaints>
{
    public ComplaintsPutDTO ComplaintsPutDTO { get; set; }

    public UpdateComplaintCommand(ComplaintsPutDTO complaintsPutDTO)
    {
        ComplaintsPutDTO = complaintsPutDTO;
    }
}
