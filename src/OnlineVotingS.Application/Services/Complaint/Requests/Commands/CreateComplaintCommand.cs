using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Commands;

public class CreateComplaintCommand : IRequest<Complaints>
{
    public ComplaintsPostDTO ComplaintsPostDTO{ get; set; }
    public CreateComplaintCommand(ComplaintsPostDTO complaintsPost)
    {
        ComplaintsPostDTO = complaintsPost;
    }
}
