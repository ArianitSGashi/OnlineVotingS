using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

    public class GetRepliedComplaintByIdQuery : IRequest<RepliedComplaints>
    {
        public int RepliedComplaintId { get; set; }

        public GetRepliedComplaintByIdQuery(int repliedComplaintId)
        {
            RepliedComplaintId = repliedComplaintId;
        }
    }