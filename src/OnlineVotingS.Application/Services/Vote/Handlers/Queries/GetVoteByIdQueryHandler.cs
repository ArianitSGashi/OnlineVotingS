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

public class GetVoteByIdQueryHandler : IRequestHandler<GetVoteByIdQuery, Votes>
{
        private readonly IVotesRepository _votesRepository;
        private readonly ILogger<GetVoteByIdQueryHandler> _logger;

        public GetVoteByIdQueryHandler(IVotesRepository votesRepository, ILogger<GetVoteByIdQueryHandler> logger)
        {
            _votesRepository = votesRepository;
            _logger = logger;
        }

        public async Task<Votes> Handle(GetVoteByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var vote = await _votesRepository.GetByIdAsync(request.VoteId);
                return vote;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching the vote with ID {VoteId}: {ErrorMessage}", request.VoteId, ex.Message);
                throw;
            }
        }
}
