using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Queries;

public class GetCampaignsByElectionIdQuery : IRequest<Result<IEnumerable<Campaign>>>
{
    public int ElectionID { get; }
    public GetCampaignsByElectionIdQuery(int electionId)
    {
        ElectionID = electionId;
    }
}