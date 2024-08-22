using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Requests.Commands;

public class DeleteRepliedComplaintCommand : IRequest<bool>
{
    public int RepliedComplaintId { get; }

    public DeleteRepliedComplaintCommand(int repliedComplaintId)
    {
        RepliedComplaintId = repliedComplaintId;
    }
}