using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByMinIncomeQuery : IRequest<IEnumerable<Candidates>>
{
    public decimal MinIncome { get;}

    public GetCandidatesByMinIncomeQuery(decimal minIncome)
    {
        MinIncome = minIncome;
    }
}