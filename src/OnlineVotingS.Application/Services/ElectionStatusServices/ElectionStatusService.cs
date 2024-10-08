﻿using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.ElectionStatusServices;

public class ElectionStatusService : IElectionStatusService
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<ElectionStatusService> _logger;

    public ElectionStatusService(IElectionRepository electionRepository, ILogger<ElectionStatusService> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task UpdateElectionStatuses()
    {
        var elections = await _electionRepository.GetAllAsync();
        var now = DateTime.Now;

        foreach (var election in elections)
        {
            var startDateTime = election.StartDate.ToDateTime(TimeOnly.FromTimeSpan(election.StartTime));
            var endDateTime = election.EndDate.ToDateTime(TimeOnly.FromTimeSpan(election.EndTime));

            // Do not change status if it's already Completed
            if (election.Status == ElectionStatus.Completed)
            {
                continue;
            }

            if (now < startDateTime && election.Status != ElectionStatus.Not_Active)
            {
                election.Status = ElectionStatus.Not_Active;
                election.UpdatedAt = DateTime.UtcNow;
                await _electionRepository.UpdateAsync(election);
            }
            else if (now >= startDateTime && now < endDateTime && election.Status != ElectionStatus.Active)
            {
                election.Status = ElectionStatus.Active;
                election.UpdatedAt = DateTime.UtcNow;
                await _electionRepository.UpdateAsync(election);
            }
            else if (now >= endDateTime && election.Status != ElectionStatus.Completed)
            {
                election.Status = ElectionStatus.Completed;
                election.UpdatedAt = DateTime.UtcNow;
                await _electionRepository.UpdateAsync(election);
            }
        }
    }
}