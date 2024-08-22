using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Requests.Queries;

public class GetRepliedComplaintByIdQuery : IRequest<GetAllRepliedComplaints>
{
    public int RepliedComplaintId { get; }
    public object RepliedComplaintsID { get; internal set; }

    public GetRepliedComplaintByIdQuery(int repliedComplaintId) => RepliedComplaintId = repliedComplaintId;
}