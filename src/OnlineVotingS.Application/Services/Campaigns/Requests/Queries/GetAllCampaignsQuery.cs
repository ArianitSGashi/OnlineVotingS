using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Queries;

public class GetAllCampaignsQuery : IRequest<IEnumerable<Campaign>>
{
}