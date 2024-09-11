using MediatR;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Application.Services.Election.Requests.Queries;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetActiveElectionsHandler : IRequestHandler<GetActiveElectionsQuery, IEnumerable<Elections>>
{
    private readonly IElectionRepository _electionsRepository;

    public GetActiveElectionsHandler(IElectionRepository electionsRepository)
    {
        _electionsRepository = electionsRepository;
    }

    public async Task<IEnumerable<Elections>> Handle(GetActiveElectionsQuery request, CancellationToken cancellationToken)
    {
        var allElections = await _electionsRepository.GetAllAsync();
        var now = DateTime.Now;

        return allElections.Where(e =>
            (e.Status == ElectionStatus.Active) ||
            (e.Status != ElectionStatus.Completed &&
             now >= e.StartDate.ToDateTime(TimeOnly.FromTimeSpan(e.StartTime)) &&
             now <= e.EndDate.ToDateTime(TimeOnly.FromTimeSpan(e.EndTime)))
        );
    }
}