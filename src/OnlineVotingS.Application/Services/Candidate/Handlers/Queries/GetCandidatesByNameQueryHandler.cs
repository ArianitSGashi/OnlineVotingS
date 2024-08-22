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

public class GetCandidatesByNameQueryHandler : IRequestHandler<GetCandidatesByNameQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByNameQueryHandler> _logger;

    public GetCandidatesByNameQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByNameQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetCandidatesByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByNameAsync(request.FullName);
            _logger.LogInformation("Successfully retrieved {Count} candidates with name {FullName}.", candidates.Count(), request.FullName);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidates with name {FullName}: {ErrorMessage}", request.FullName, ex.Message);
            throw;
        }
    }
}

