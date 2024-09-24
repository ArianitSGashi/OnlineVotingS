using FluentResults;
using MediatR;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Services.Campaigns.Requests.Queries;

public class GetAllCampaignsQuery : IRequest<Result<IEnumerable<Campaign>>>
{
}