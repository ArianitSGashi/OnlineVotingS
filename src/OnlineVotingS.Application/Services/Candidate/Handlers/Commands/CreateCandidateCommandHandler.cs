using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.CostumExceptions;

public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Candidates>
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

    public async Task<Candidates> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = _mapper.Map<Candidates>(request.CandidateDto);
            await _candidateRepository.AddAsync(candidate);
            return candidate;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx &&
                                           (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            throw new DuplicateCandidateException($"A candidate with the name '{request.CandidateDto.FullName}' " +
                $"already exists in election {request.CandidateDto.ElectionID} for party {request.CandidateDto.Party}.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a candidate");
            throw;
        }
    }
}