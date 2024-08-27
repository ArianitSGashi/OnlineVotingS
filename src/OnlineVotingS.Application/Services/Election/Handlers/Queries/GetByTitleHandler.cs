using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Election.Handlers.Queries;

public class GetByTitleHandler : IRequestHandler<GetByTitleQuery, IEnumerable<Elections>>
    {
        private readonly IElectionRepository _electionRepository;
        private readonly ILogger<GetByTitleHandler> _logger;

        public GetByTitleHandler(IElectionRepository electionRepository, ILogger<GetByTitleHandler> logger)
        {
            _electionRepository = electionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Elections>> Handle(GetByTitleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _electionRepository.GetByTitleAsync(request.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching title: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }