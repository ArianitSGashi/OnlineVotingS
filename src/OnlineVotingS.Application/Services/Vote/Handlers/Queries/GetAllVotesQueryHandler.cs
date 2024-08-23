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

public class GetAllVotesQueryHandler : IRequestHandler<GetAllVotesQuery, IEnumerable<Votes>>
{
    private readonly IVotesRepository _votesRepository;
    private readonly ILogger<GetAllVotesQueryHandler> _logger;

    public GetAllVotesQueryHandler(IVotesRepository votesRepository, ILogger<GetAllVotesQueryHandler> logger)
    {
        _votesRepository = votesRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Votes>> Handle(GetAllVotesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var votes = await _votesRepository.GetAllAsync();
            return votes;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching all votes: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
