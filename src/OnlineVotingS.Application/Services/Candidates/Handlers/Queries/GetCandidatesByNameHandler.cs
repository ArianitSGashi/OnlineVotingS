using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidates.Handlers.Queries;

public class GetCandidatesByNameHandler : IRequestHandler<GetCandidatesByNameQuery, IEnumerable<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidatesByNameHandler> _logger;

    public GetCandidatesByNameHandler(ICandidateRepository candidateRepository, ILogger<GetCandidatesByNameHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Candidates>> Handle(GetCandidatesByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidates = await _candidateRepository.GetByNameAsync(request.Name);
            return candidates;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching candidates with name {Name}: {ErrorMessage}", request.Name, ex.Message);
            throw;
        }
    }
}
