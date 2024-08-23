using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class GetVotesByUserIDQueryHandler : IRequestHandler<GetVotesByUserIDQuery, IEnumerable<Votes>>
{
        private readonly IVotesRepository _votesRepository;
        private readonly ILogger<GetVotesByUserIDQueryHandler> _logger;

        public GetVotesByUserIDQueryHandler(IVotesRepository votesRepository, ILogger<GetVotesByUserIDQueryHandler> logger)
        {
            _votesRepository = votesRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Votes>> Handle(GetVotesByUserIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var votes = await _votesRepository.GetByUserIDAsync(request.UserID);
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching votes for user ID {UserID}: {ErrorMessage}", request.UserID, ex.Message);
                throw;
            }
        }
}