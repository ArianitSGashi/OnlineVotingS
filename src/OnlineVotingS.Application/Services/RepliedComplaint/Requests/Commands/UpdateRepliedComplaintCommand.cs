using MediatR;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;

public class UpdateRepliedComplaintCommand : IRequest<RepliedComplaints>
{
    public RepliedComplaintsPutDTO RepliedComplaint { get; }

    public UpdateRepliedComplaintCommand(RepliedComplaintsPutDTO repliedComplaint)
    {
        RepliedComplaint = repliedComplaint;
    }
}