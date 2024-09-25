using FluentResults;
using static FluentResults.Result;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.CostumExceptions;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Commands;

public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Result<Candidates>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCandidateCommandHandler> _logger;

    public CreateCandidateCommandHandler(
        ICandidateRepository candidateRepository,
        IMapper mapper,
        ILogger<CreateCandidateCommandHandler> logger)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<Candidates>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.CandidateDto?.FullName))
        {
            return new Result<Candidates>().WithError(ErrorCodes.CANDIDATE_NOT_FOUND.ToString());
        }

        try
        {
            var candidate = _mapper.Map<Candidates>(request.CandidateDto);
            await _candidateRepository.AddAsync(candidate);
            return Ok(candidate);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx &&
                                           (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            var errorMessage = $"A candidate already exists in election {request.CandidateDto.ElectionID} for party {request.CandidateDto.Party}.";
            return new Result<Candidates>().WithError(new DuplicateCandidateException(errorMessage).Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a candidate");
            return new Result<Candidates>().WithError(ErrorCodes.CANDIDATE_CREATION_FAILED.ToString());
        }
    }
}
