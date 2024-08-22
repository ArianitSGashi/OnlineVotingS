using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

    public class GetByComplaintIDQuery : IRequest<IEnumerable<RepliedComplaints>>
    {
        public int ComplaintID { get; set; }

        public GetByComplaintIDQuery(int complaintID)
        {
            ComplaintID = complaintID;
        }
    }