using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Results.Handlers.Queries;

public class GetResultsByCandidateIdHandler : IRequestHandler<GetResultsByCandidateIdQuery, IEnumerable<Result>>
{
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GetResultsByCandidateIdHandler> _logger;

    public GetResultsByCandidateIdHandler(IResultRepository resultRepository, ILogger<GetResultsByCandidateIdHandler> logger)
    {
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Result>> Handle(GetResultsByCandidateIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var results = await _resultRepository.GetByCandidateIdAsync(request.CandidateId);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching results for Candidate ID {CandidateId}: {ErrorMessage}", request.CandidateId, ex.Message);
            throw;
        }
    }
}