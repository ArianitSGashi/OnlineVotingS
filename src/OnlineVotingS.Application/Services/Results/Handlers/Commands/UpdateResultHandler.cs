using static FluentResults.Result;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using ResultEntity = OnlineVotingS.Domain.Entities.Result;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class UpdateResultHandler : IRequestHandler<UpdateResultCommand, Result<ResultEntity>>
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

    public async Task<Result<ResultEntity>> Handle(UpdateResultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingResult = await _resultRepository.GetByIdAsync(request.ResultDto.ResultID);
            if (existingResult == null)
            {
                var errorMessage = $"Result with ID {request.ResultDto.ResultID} not found.";
                return new Result<ResultEntity>().WithError(errorMessage);
            }

            _mapper.Map(request.ResultDto, existingResult);
            await _resultRepository.UpdateAsync(existingResult);
            return Ok(existingResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the result with ID {ResultId}", request.ResultDto.ResultID);
            return new Result<ResultEntity>().WithError(ErrorCodes.RESULT_UPDATE_FAILED.ToString());
        }
    }
}
