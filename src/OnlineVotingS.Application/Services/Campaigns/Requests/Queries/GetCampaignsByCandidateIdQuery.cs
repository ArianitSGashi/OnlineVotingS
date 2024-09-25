using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Queries;

public class GetCampaignsByCandidateIdQuery : IRequest<Result<IEnumerable<Campaign>>>
{
    public int CandidateID { get; }
    public GetCampaignsByCandidateIdQuery(int candidateId)
    {
        CandidateID = candidateId;
    }
}