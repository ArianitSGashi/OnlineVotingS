using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class UpdateElectionsHandler : IRequestHandler<UpdateElectionsCommand, FluentResults.Result<Elections>>
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

    public async Task<FluentResults.Result<Elections>> Handle(UpdateElectionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var elections = await _electionsRepository.GetByIdAsync(request.ElectionDto.ElectionID);
            if (elections == null)
            {
                var errorMessage = $"Election with ID {request.ElectionDto.ElectionID} not found.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage); 
            }

            _mapper.Map(request.ElectionDto, elections);
            await _electionsRepository.UpdateAsync(elections);

            _logger.LogInformation("Election with ID {ElectionId} successfully updated.", request.ElectionDto.ElectionID);
            return FluentResults.Result.Ok(elections); 
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the election with ID {ElectionId}: {ErrorMessage}", request.ElectionDto.ElectionID, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex)); 
        }
    }
}
