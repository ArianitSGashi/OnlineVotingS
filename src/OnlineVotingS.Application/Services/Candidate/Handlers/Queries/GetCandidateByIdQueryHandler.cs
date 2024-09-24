using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries;

public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, Result<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<GetCandidateByIdQueryHandler> _logger;

    public GetCandidateByIdQueryHandler(ICandidateRepository candidateRepository, ILogger<GetCandidateByIdQueryHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Result<Candidates>> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);
            if (candidate == null)
            {
                return new Result<Candidates>().WithError(new Error(ErrorCodes.CANDIDATE_NOT_FOUND.ToString())
                    .WithMetadata("CandidateId", request.CandidateId));
            }
            return Ok(candidate);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while retrieving candidate with ID {CandidateId}: {ErrorMessage}", request.CandidateId, ex.Message);
            return new Result<Candidates>().WithError(new Error(ErrorCodes.CANDIDATE_NOT_FOUND.ToString())
                .WithMetadata("CandidateId", request.CandidateId)
                .WithMetadata("ExceptionMessage", ex.Message));
        }
    }
}