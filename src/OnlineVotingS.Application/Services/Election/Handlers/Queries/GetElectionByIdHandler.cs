using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetElectionByIdHandler : IRequestHandler<GetElectionsByIdQuery, Elections>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetElectionByIdHandler> _logger;

    public GetElectionByIdHandler(IElectionRepository electionRepository, ILogger<GetElectionByIdHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<Elections> Handle(GetElectionsByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var election = await _electionRepository.GetByIdAsync(request.ElectionID);
            if (election == null)
            {
                throw new KeyNotFoundException($"Election with ID {request.ElectionID} not found.");
            }
            return election;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the election with ID {ElectionId}: {ErrorMessage}", request.ElectionID, ex.Message);
            throw;
        }
    }
}