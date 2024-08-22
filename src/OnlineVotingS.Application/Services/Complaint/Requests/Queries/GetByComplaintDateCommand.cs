using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Complaint.Requests.Queries;

public class GetByComplaintDateCommand : IRequest<IEnumerable<Complaints>>
{
    public DateTime Date { get; set; }

    public GetByComplaintDateCommand(DateTime date)
    {
        Date = date;
    }
}
