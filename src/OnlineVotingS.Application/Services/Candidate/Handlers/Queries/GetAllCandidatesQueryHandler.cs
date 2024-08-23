using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetAllCandidatesQueryHandler> _logger;

    public GetAllCandidatesQueryHandler(ICandidateRepository candidateRepository, ILogger<GetAllCandidatesQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all candidates: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}