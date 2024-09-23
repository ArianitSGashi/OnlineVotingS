using static FluentResults.Result;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class GenerateOrUpdateResultsHandler : IRequestHandler<GenerateOrUpdateResultsCommand, Result>
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

    public async Task<Result> Handle(GenerateOrUpdateResultsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var votesForElection = await _mediator.Send(new GetVotesByElectionIDQuery(request.ElectionID), cancellationToken);

            if (request.CandidateID.HasValue)
            {
                votesForElection = votesForElection.Where(x => x.CandidateID == request.CandidateID);
            }

            var votesBasedOnCandidates = votesForElection.GroupBy(x => x.CandidateID);

            foreach (var voteGroup in votesBasedOnCandidates)
            {
                var existingResult = await _resultRepository.GetResultByCandidateAndElectionAsync(voteGroup.Key, request.ElectionID);

                if (existingResult != null)
                {
                    var updateResult = await _mediator.Send(new UpdateResultCommand(new ResultPutDTO
                    {
                        ResultID = existingResult.ResultID,
                        TotalVotes = voteGroup.Count(),
                        CandidateID = voteGroup.Key,
                        ElectionID = request.ElectionID
                    }), cancellationToken);

                    if (updateResult.IsFailed)
                    {
                        return new Result().WithError("Failed to update existing result.");
                    }
                }
                else
                {
                    var createResult = await _mediator.Send(new CreateResultCommand(new ResultPostDTO
                    {
                        TotalVotes = voteGroup.Count(),
                        CandidateID = voteGroup.Key,
                        ElectionID = request.ElectionID
                    }), cancellationToken);

                    if (createResult.IsFailed)
                    {
                        return new Result().WithError("Failed to create new result.");
                    }
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while generating or updating results for ElectionID: {ElectionID}", request.ElectionID);
            return new Result().WithError(ErrorCodes.RESULT_UPDATE_FAILED.ToString());
        }
    }
}
