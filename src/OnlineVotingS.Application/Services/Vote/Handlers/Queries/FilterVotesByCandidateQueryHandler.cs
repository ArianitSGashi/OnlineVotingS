using MediatR;
using OnlineVotingS.Application.DTO.GetDTO;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class FilterVotesByCandidateQueryHandler : IRequestHandler<FilterVotesByCandidateQuery, IEnumerable<CandidateVotes>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ICandidateRepository _candidateRepository;

    public FilterVotesByCandidateQueryHandler(IVotesRepository votesRepository, ICandidateRepository candidateRepository)
    {
        _votesRepository = votesRepository;
        _candidateRepository = candidateRepository;
    }

    public async Task<IEnumerable<CandidateVotes>> Handle(FilterVotesByCandidateQuery request, CancellationToken cancellationToken)
    {
        var listOfCandidateVotes = new List<CandidateVotes>();
        var allVotesByElection = await _votesRepository.GetByElectionIDAsync(request.ElectionId);
        var votesGrouped = allVotesByElection.GroupBy(x => x.CandidateID);
        foreach (var item in votesGrouped)
        {
            var candidate = await _candidateRepository.GetByIdAsync(item.Key);
            
            listOfCandidateVotes.Add(new CandidateVotes()
            {
                CandidateFullName = candidate.FullName,
                Votes = item.Count()
            });
        }

        return listOfCandidateVotes.AsEnumerable();
    }
}