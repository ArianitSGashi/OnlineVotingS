using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Candidate.Requests.Queries;

public class GetCandidatesByMinIncomeQuery : IRequest<Result<IEnumerable<Candidates>>>
{
    public decimal MinIncome { get;}

    public GetCandidatesByMinIncomeQuery(decimal minIncome)
    {
        MinIncome = minIncome;
    }
}