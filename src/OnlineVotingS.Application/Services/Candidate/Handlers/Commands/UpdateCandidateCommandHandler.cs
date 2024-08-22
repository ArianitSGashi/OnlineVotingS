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

public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, Candidates>
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

    public async Task<Candidates> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateDto.CandidateID);
            if (candidate == null)
            {
                _logger.LogWarning("Candidate with ID {CandidateId} not found.", request.CandidateDto.CandidateID);
                throw new KeyNotFoundException($"Candidate with ID {request.CandidateDto.CandidateID} not found.");
            }

            _mapper.Map(request.CandidateDto, candidate);
            await _candidateRepository.UpdateAsync(candidate);

            _logger.LogInformation("Candidate with ID {CandidateId} updated successfully.", request.CandidateDto.CandidateID);
            return candidate;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the candidate with ID {CandidateId}: {ErrorMessage}", request.CandidateDto.CandidateID, ex.Message);
            throw;
        }
    }
}
