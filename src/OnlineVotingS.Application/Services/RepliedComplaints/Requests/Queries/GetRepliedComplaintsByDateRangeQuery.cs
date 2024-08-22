using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Requests.Queries;

public class GetRepliedComplaintsByDateRangeQuery : IRequest<IEnumerable<GetAllRepliedComplaints>>
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public GetRepliedComplaintsByDateRangeQuery(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}