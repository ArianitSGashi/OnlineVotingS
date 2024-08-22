using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class CreateResultHandler : IRequestHandler<CreateResultCommand, Result>
{
    private readonly IResultRepository _resultRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateResultHandler> _logger;

    public CreateResultHandler(IResultRepository resultRepository, IMapper mapper, ILogger<CreateResultHandler> logger)
    {
        _resultRepository = resultRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result> Handle(CreateResultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _mapper.Map<Result>(request.ResultDto);
            await _resultRepository.AddAsync(result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a result: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}