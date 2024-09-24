using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class GetAllVotesQueryHandler : IRequestHandler<GetAllVotesQuery, Result<IEnumerable<Votes>>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<GetAllVotesQueryHandler> _logger;

    public GetAllVotesQueryHandler(IVotesRepository votesRepository, ILogger<GetAllVotesQueryHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Votes>>> Handle(GetAllVotesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var votes = await _votesRepository.GetAllAsync();
            return Ok(votes);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all votes: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<Votes>>().WithError(ErrorCodes.VOTE_NOT_FOUND.ToString());
        }
    }
}