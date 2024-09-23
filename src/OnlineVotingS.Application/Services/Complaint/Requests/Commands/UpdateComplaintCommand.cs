using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Commands;

public class UpdateComplaintCommand : IRequest<Result<Complaints>>
{
    public ComplaintsPutDTO ComplaintsPutDTO { get; }

    public UpdateComplaintCommand(ComplaintsPutDTO complaintsPutDTO)
    {
        ComplaintsPutDTO = complaintsPutDTO;
    }
}
