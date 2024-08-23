using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Vote.Handlers.Commands;

public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, Votes>
{
    private readonly IVotesRepository _votesRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateVoteCommandHandler> _logger;

    public CreateVoteCommandHandler(IVotesRepository votesRepository, IMapper mapper, ILogger<CreateVoteCommandHandler> logger)
    {
        _votesRepository = votesRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Votes> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var vote = _mapper.Map<Votes>(request.VoteDto);
            await _votesRepository.AddAsync(vote);
            return vote;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a vote: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
