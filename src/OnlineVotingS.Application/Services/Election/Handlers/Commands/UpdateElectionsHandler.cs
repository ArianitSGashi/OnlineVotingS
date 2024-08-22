using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class UpdateElectionsHandler : IRequestHandler<UpdateElectionsCommand, Elections>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateElectionsHandler> _logger;

    public UpdateElectionsHandler(IElectionRepository electionsRepository, IMapper mapper, ILogger<UpdateElectionsHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Elections> Handle(UpdateElectionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var elections = await _electionsRepository.GetByIdAsync(request.ElectionDto.ElectionID);
            if (elections == null)
            {
                _logger.LogWarning("Elections with ID {ElectionId} not found.", request.ElectionDto.ElectionID);
                throw new KeyNotFoundException($"Elections with ID {request.ElectionDto.ElectionID} not found.");
            }

            _mapper.Map(request.ElectionDto, elections);
            await _electionsRepository.UpdateAsync(elections);

            _logger.LogInformation("Elections with ID {ElectionId} updated successfully.", request.ElectionDto.ElectionID);
            return elections;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the elections with ID {ElectionId}: {ErrorMessage}", request.ElectionDto.ElectionID, ex.Message);
            throw;
        }
    }
}
