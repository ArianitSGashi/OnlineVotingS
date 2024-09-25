using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class GetVotesByCandidateIDQueryHandler : IRequestHandler<GetVotesByCandidateIDQuery, Result<IEnumerable<Votes>>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<GetVotesByCandidateIDQueryHandler> _logger;

    public GetVotesByCandidateIDQueryHandler(IVotesRepository votesRepository, ILogger<GetVotesByCandidateIDQueryHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Votes>>> Handle(GetVotesByCandidateIDQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var votes = await _votesRepository.GetByCandidateIDAsync(request.CandidateID);
            return Ok(votes);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching votes for candidate ID {CandidateID}: {ErrorMessage}", request.CandidateID, ex.Message);
            return new Result<IEnumerable<Votes>>().WithError(ErrorCodes.VOTE_NOT_FOUND.ToString());
        }
    }
}