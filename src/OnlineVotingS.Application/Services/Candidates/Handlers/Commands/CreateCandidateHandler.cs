using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Candidates.Handlers.Commands;

public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, Candidates>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCandidateHandler> _logger;

    public CreateCandidateHandler(ICandidateRepository candidateRepository, IMapper mapper, ILogger<CreateCandidateHandler> logger)
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

            _logger.LogInformation("Candidate created successfully with ID {CandidateId}.", candidate.CandidateID);
            return candidate;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a candidate: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
