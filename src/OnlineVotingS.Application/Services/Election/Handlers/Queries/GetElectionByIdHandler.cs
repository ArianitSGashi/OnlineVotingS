using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetElectionByIdHandler : IRequestHandler<GetElectionsByIdQuery, Result<Elections>>
{
    private readonly IElectionRepository _electionRepository;
    private readonly ILogger<GetElectionByIdHandler> _logger;

    public GetElectionByIdHandler(IElectionRepository electionRepository, ILogger<GetElectionByIdHandler> logger)
    {
        _electionRepository = electionRepository;
        _logger = logger;
    }

    public async Task<Result<Elections>> Handle(GetElectionsByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var election = await _electionRepository.GetByIdAsync(request.ElectionID);
            if (election == null)
            {
                return new Result<Elections>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
            }
            return Ok(election);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the election with ID {ElectionId}: {ErrorMessage}", request.ElectionID, ex.Message);
            return new Result<Elections>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
        }
    }
}