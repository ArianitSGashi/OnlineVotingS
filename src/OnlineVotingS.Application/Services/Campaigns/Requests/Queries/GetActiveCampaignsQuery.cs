using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Queries;

public class GetActiveCampaignsQuery : IRequest<Result<IEnumerable<Campaign>>>
{
}