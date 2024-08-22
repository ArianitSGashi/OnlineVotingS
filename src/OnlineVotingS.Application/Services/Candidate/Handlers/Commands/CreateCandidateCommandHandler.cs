using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Commands;

public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Candidates>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCandidateCommandHandler> _logger;

    public CreateCandidateCommandHandler(ICandidateRepository candidateRepository, IMapper mapper, ILogger<CreateCandidateCommandHandler> logger)
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
