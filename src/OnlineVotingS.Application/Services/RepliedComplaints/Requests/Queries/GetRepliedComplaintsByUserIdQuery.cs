using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Requests.Queries;

public class GetRepliedComplaintsByUserIdQuery : IRequest<IEnumerable<GetAllRepliedComplaints>>
{
    public int UserId { get; }

    public GetRepliedComplaintsByUserIdQuery(int userId)
    {
        UserId = userId;
    }
}