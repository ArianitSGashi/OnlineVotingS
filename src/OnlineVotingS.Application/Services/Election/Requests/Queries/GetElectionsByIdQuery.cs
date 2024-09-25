using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Election.Requests.Queries;

public class GetElectionsByIdQuery : IRequest<Result<Elections>>
{
    public int ElectionID { get; }

    public GetElectionsByIdQuery(int electionId)
    {
        ElectionID = electionId;
    }
}