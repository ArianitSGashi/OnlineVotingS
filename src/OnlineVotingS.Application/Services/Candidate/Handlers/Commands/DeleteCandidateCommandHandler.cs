using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Domain.Errors;
using OnlineVotingS.Domain.Interfaces;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Commands;

public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, Result<bool>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<DeleteCandidateCommandHandler> _logger;

    public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository, ILogger<DeleteCandidateCommandHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _candidateRepository.ExistsAsync(request.CandidateId);
            if (!exists)
            {
                return new Result<bool>().WithError(ErrorCodes.CANDIDATE_NOT_FOUND.ToString());
            }
            await _candidateRepository.DeleteAsync(request.CandidateId);
            return Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the candidate with ID '{request.CandidateId}': {ex.Message}");
            return new Result<bool>().WithError(ErrorCodes.CANDIDATE_DELETION_FAILED.ToString());
        }
    }
}
