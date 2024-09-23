using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class UpdateResultHandler : IRequestHandler<UpdateResultCommand, Result<OnlineVotingS.Domain.Entities.Result>>
{
    private readonly IResultRepository _resultRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateResultHandler> _logger;

    public UpdateResultHandler(IResultRepository resultRepository, IMapper mapper, ILogger<UpdateResultHandler> logger)
    {
        _resultRepository = resultRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<OnlineVotingS.Domain.Entities.Result>> Handle(UpdateResultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingResult = await _resultRepository.GetByIdAsync(request.ResultDto.ResultID);
            if (existingResult == null)
            {
                var errorMessage = $"Result with ID {request.ResultDto.ResultID} not found.";
                _logger.LogWarning(errorMessage);
                return FluentResults.Result.Fail(errorMessage);  
            }

            _mapper.Map(request.ResultDto, existingResult);
            await _resultRepository.UpdateAsync(existingResult);
            _logger.LogInformation("Result with ID {ResultId} was successfully updated.", request.ResultDto.ResultID);
            return FluentResults.Result.Ok(existingResult);  
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the result with ID {ResultId}", request.ResultDto.ResultID);
            return FluentResults.Result.Fail(new ExceptionalError(ex));  
        }
    }
}
