using FluentResults;
using static FluentResults.Result;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Commands;

public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, Result<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCandidateCommandHandler> _logger;

    public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository, IMapper mapper, ILogger<UpdateCandidateCommandHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<Candidates>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateDto.CandidateID);
            if (candidate == null)
            {
                return new Result<Candidates>().WithError(ErrorCodes.CANDIDATE_NOT_FOUND.ToString());
            }

            _mapper.Map(request.CandidateDto, candidate);
            await _candidateRepository.UpdateAsync(candidate);
            return Ok(candidate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the candidate with ID {CandidateID}: {ErrorMessage}", request.CandidateDto.CandidateID, ex.Message);
            return new Result<Candidates>().WithError(ErrorCodes.CANDIDATE_UPDATE_FAILED.ToString());
        }
    }
}
