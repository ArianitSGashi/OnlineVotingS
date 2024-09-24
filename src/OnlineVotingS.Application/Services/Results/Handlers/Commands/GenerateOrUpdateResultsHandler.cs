using FluentResults;
using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using Microsoft.Extensions.Logging;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class GenerateOrUpdateResultsHandler : IRequestHandler<GenerateOrUpdateResultsCommand, Result<Unit>>
{
    private readonly IMediator _mediator;
    private readonly IResultRepository _resultRepository;
    private readonly ILogger<GenerateOrUpdateResultsHandler> _logger;

    public GenerateOrUpdateResultsHandler(IMediator mediator, IResultRepository resultRepository, ILogger<GenerateOrUpdateResultsHandler> logger)
    {
        _mediator = mediator;
        _resultRepository = resultRepository;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(GenerateOrUpdateResultsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var votesResult = await _mediator.Send(new GetVotesByElectionIDQuery(request.ElectionID));
            if (votesResult.IsFailed)
            {
                return new Result<Unit>().WithError(votesResult.Errors.First().Message);
            }

            var votesForElection = votesResult.Value;
            if (request.CandidateID.HasValue)
            {
                votesForElection = votesForElection.Where(x => x.CandidateID == request.CandidateID);
            }

            var votesBasedOnCandidates = votesForElection.GroupBy(x => x.CandidateID);
            foreach (var vote in votesBasedOnCandidates)
            {
                var existingResult = await _resultRepository.GetResultByCandidateAndElectionAsync(vote.Key, request.ElectionID);
                if (existingResult != null)
                {
                    var updateResult = await _mediator.Send(new UpdateResultCommand(new ResultPutDTO
                    {
                        ResultID = existingResult.ResultID,
                        TotalVotes = vote.Count(),
                        CandidateID = vote.Key,
                        ElectionID = request.ElectionID
                    }));

                    if (updateResult.IsFailed)
                    {
                        return new Result<Unit>().WithError(updateResult.Errors.First().Message);
                    }
                }
                else
                {
                    var createResult = await _mediator.Send(new CreateResultCommand(new ResultPostDTO
                    {
                        TotalVotes = vote.Count(),
                        CandidateID = vote.Key,
                        ElectionID = request.ElectionID
                    }));

                    if (createResult.IsFailed)
                    {
                        return new Result<Unit>().WithError(createResult.Errors.First().Message);
                    }
                }
            }

            return Result.Ok(Unit.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while generating or updating results for election ID {ElectionID}", request.ElectionID);
            return new Result<Unit>().WithError(ErrorCodes.RESULT_CREATION_FAILED.ToString());
        }
    }
}