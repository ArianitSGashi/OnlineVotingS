using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands;

public class CreateElectionsHandler : IRequestHandler<CreateElectionsCommand, Elections>
{
    private readonly IElectionRepository _electionsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateElectionsHandler> _logger;

    public CreateElectionsHandler(IElectionRepository electionsRepository, IMapper mapper, ILogger<CreateElectionsHandler> logger)
    {
        _electionsRepository = electionsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Elections> Handle(CreateElectionsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var elections = _mapper.Map<Elections>(request.ElectionDto);
            await _electionsRepository.AddAsync(elections);

            return elections;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating elections: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
