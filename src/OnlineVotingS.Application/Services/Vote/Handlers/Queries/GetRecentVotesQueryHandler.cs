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

public class GetRecentVotesQueryHandler : IRequestHandler<GetRecentVotesQuery, IEnumerable<Votes>>
{
        private readonly IVotesRepository _votesRepository;
        private readonly ILogger<GetRecentVotesQueryHandler> _logger;

        public GetRecentVotesQueryHandler(IVotesRepository votesRepository, ILogger<GetRecentVotesQueryHandler> logger)
        {
            _votesRepository = votesRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Votes>> Handle(GetRecentVotesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var votes = await _votesRepository.GetRecentVotesAsync(request.Date);
                return votes;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching recent votes: {ErrorMessage}", ex.Message);
                throw;
            }
        }
}
