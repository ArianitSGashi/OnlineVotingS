using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Queries;

public class GetVotesByElectionIDQueryHandler : IRequestHandler<GetVotesByElectionIDQuery, IEnumerable<Votes>>
{
        private readonly IVotesRepository _votesRepository;
        private readonly ILogger<GetVotesByElectionIDQueryHandler> _logger;

        public GetVotesByElectionIDQueryHandler(IVotesRepository votesRepository, ILogger<GetVotesByElectionIDQueryHandler> logger)
        {
            _votesRepository = votesRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Votes>> Handle(GetVotesByElectionIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var votes = await _votesRepository.GetByElectionIDAsync(request.ElectionID);
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching votes for election ID {ElectionID}: {ErrorMessage}", request.ElectionID, ex.Message);
                throw;
            }
        }
}
