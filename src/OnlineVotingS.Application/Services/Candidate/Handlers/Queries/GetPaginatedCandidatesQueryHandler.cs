using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries
{
    public class GetPaginatedCandidatesQueryHandler : IRequestHandler<GetPaginatedCandidatesQuery, PaginatedList<Candidates>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetPaginatedCandidatesQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<PaginatedList<Candidates>> Handle(GetPaginatedCandidatesQuery request, CancellationToken cancellationToken)
        {
            var totalCandidates = await _candidateRepository.GetTotalCandidatesCountAsync();
            var totalPages = (int)Math.Ceiling(totalCandidates / (double)request.PageSize);
            var candidates = await _candidateRepository.GetCandidatesPaginatedAsync(request.PageNumber, request.PageSize);

            return new PaginatedList<Candidates>(candidates.ToList(), request.PageNumber, totalPages, totalCandidates);
        }
    }
}
