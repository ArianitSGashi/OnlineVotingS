using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Commands;

public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, FluentResults.Result>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<DeleteCandidateCommandHandler> _logger;

    public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository, ILogger<DeleteCandidateCommandHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<FluentResults.Result> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _candidateRepository.ExistsAsync(request.CandidateId);
            if (!exists)
            {
                return FluentResults.Result.Fail($"Candidate with ID {request.CandidateId} not found.");
            }

            await _candidateRepository.DeleteAsync(request.CandidateId);
            return FluentResults.Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting the candidate with ID {CandidateId}: {ErrorMessage}", request.CandidateId, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
