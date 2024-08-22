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

public class GetCandidatesByElectionIdQueryHandler : IRequestHandler<GetCandidatesByElectionIdQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByElectionIdQueryHandler> _logger;

    public GetCandidatesByElectionIdQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByElectionIdQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetCandidatesByElectionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByElectionIdAsync(request.ElectionId);
            _logger.LogInformation("Successfully retrieved {Count} candidates for Election ID {ElectionId}.", candidates.Count(), request.ElectionId);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidates for Election ID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            throw;
        }
    }
}


