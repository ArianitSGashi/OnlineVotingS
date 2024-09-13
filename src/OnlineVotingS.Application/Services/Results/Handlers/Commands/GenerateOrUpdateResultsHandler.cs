using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class GenerateOrUpdateResultsHandler : IRequestHandler<GenerateOrUpdateResultsCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IResultRepository _resultRepository;

    public GenerateOrUpdateResultsHandler(IMediator mediator, IResultRepository resultRepository)
    {
        _mediator = mediator;
        _resultRepository = resultRepository;
    }

    public async Task<Unit> Handle(GenerateOrUpdateResultsCommand request, CancellationToken cancellationToken)
    {
        var votesForElection = await _mediator.Send(new GetVotesByElectionIDQuery(request.ElectionID));

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
                await _mediator.Send(new UpdateResultCommand(new ResultPutDTO
                {
                    ResultID = existingResult.ResultID,
                    TotalVotes = vote.Count(),
                    CandidateID = vote.Key,
                    ElectionID = request.ElectionID
                }));
            }
            else
            {
                await _mediator.Send(new CreateResultCommand(new ResultPostDTO
                {
                    TotalVotes = vote.Count(),
                    CandidateID = vote.Key,
                    ElectionID = request.ElectionID
                }));
            }
        }

        return Unit.Value;
    }
}