using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByMinIncomeQuery : IRequest<IEnumerable<Candidates>>
{
    public decimal MinIncome { get; set; }

    public GetCandidatesByMinIncomeQuery(decimal minIncome)
    {
        MinIncome = minIncome;
    }
}
