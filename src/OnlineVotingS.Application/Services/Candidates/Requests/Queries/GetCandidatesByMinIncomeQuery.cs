using MediatR;
using System.Collections.Generic;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidates.Requests.Queries;

public class GetCandidatesByMinIncomeQuery : IRequest<IEnumerable<Candidates>>
{
    public decimal MinIncome { get; }

    public GetCandidatesByMinIncomeQuery(decimal minIncome)
    {
        MinIncome = minIncome;
    }
}
