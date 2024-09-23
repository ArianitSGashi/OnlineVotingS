using static FluentResults.Result;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class CreateResultHandler : IRequestHandler<CreateResultCommand, Result<OnlineVotingS.Domain.Entities.Result>>
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

    public async Task<Result<OnlineVotingS.Domain.Entities.Result>> Handle(CreateResultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var resultEntity = _mapper.Map<OnlineVotingS.Domain.Entities.Result>(request.ResultDto);
            await _resultRepository.AddAsync(resultEntity);
            return Ok(resultEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a result: {ErrorMessage}", ex.Message);
            return new Result<OnlineVotingS.Domain.Entities.Result>().WithError(ErrorCodes.RESULT_CREATION_FAILED.ToString());
        }
    }
}