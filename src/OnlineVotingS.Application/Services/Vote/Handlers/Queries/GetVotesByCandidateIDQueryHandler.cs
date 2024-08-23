using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class GetVotesByCandidateIDQueryHandler : IRequestHandler<GetVotesByCandidateIDQuery, IEnumerable<Votes>>
{
        private readonly IVotesRepository _votesRepository;
        private readonly ILogger<GetVotesByCandidateIDQueryHandler> _logger;

        public GetVotesByCandidateIDQueryHandler(IVotesRepository votesRepository, ILogger<GetVotesByCandidateIDQueryHandler> logger)
        {
            _votesRepository = votesRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Votes>> Handle(GetVotesByCandidateIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var votes = await _votesRepository.GetByCandidateIDAsync(request.CandidateID);
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching votes for candidate ID {CandidateID}: {ErrorMessage}", request.CandidateID, ex.Message);
                throw;
            }
        }
}