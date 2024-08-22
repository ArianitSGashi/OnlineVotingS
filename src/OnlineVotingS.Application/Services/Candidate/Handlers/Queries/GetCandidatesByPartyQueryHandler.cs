using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetCandidatesByPartyQueryHandler : IRequestHandler<GetCandidatesByPartyQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByPartyQueryHandler> _logger;

    public GetCandidatesByPartyQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByPartyQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetCandidatesByPartyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByPartyAsync(request.PartyName);
            _logger.LogInformation("Successfully retrieved {Count} candidates for party {PartyName}.", candidates.Count(), request.PartyName);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidates for party {PartyName}: {ErrorMessage}", request.PartyName, ex.Message);
            throw;
        }
    }
}

