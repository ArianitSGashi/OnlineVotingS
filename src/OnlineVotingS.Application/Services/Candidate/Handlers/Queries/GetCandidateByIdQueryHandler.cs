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

public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, Candidates>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidateByIdQueryHandler> _logger;

    public GetCandidateByIdQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidateByIdQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Candidates> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);
            if (candidate == null)
            {
                _logger.LogWarning("Candidate with ID {CandidateId} not found.", request.CandidateId);
                throw new KeyNotFoundException($"Candidate with ID {request.CandidateId} not found.");
            }
            return candidate;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidate with ID {CandidateId}: {ErrorMessage}", request.CandidateId, ex.Message);
            throw;
        }
    }
}
