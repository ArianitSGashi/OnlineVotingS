using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Campaign.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Candidate.Handlers.Queries
{
    public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, IEnumerable<Candidates>>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ILogger<GetAllCandidatesQueryHandler> _logger;

        public GetAllCandidatesQueryHandler(ICandidateRepository candidateRepository, ILogger<GetAllCandidatesQueryHandler> logger)
        {
            _candidateRepository = candidateRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Candidates>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _candidateRepository.GetAllAsync();
        }
    }
}
