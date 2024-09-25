using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;

public class UpdateRepliedComplaintCommand : IRequest<Result<RepliedComplaints>>
{
    public RepliedComplaintsPutDTO RepliedComplaint { get; }

    public UpdateRepliedComplaintCommand(RepliedComplaintsPutDTO repliedComplaint)
    {
        RepliedComplaint = repliedComplaint;
    }
}
