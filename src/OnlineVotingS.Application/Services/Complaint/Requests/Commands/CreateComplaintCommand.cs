using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Commands;

public class CreateComplaintCommand : IRequest<Result<Complaints>>
{
    public ComplaintsPostDTO ComplaintsPostDTO { get; }

    public CreateComplaintCommand(ComplaintsPostDTO complaintsPost)
    {
        ComplaintsPostDTO = complaintsPost;
    }
}
