using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Results.Handlers.Commands;

public class UpdateResultHandler : IRequestHandler<UpdateResultCommand, Result>
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

    public async Task<Result> Handle(UpdateResultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _resultRepository.GetByIdAsync(request.ResultDto.ResultID);
            if (result == null)
            {
                throw new KeyNotFoundException($"Result with ID {request.ResultDto.ResultID} not found.");
            }

            _mapper.Map(request.ResultDto, result);
            await _resultRepository.UpdateAsync(result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the result with ID {ResultId}: {ErrorMessage}", request.ResultDto.ResultID, ex.Message);
            throw;
        }
    }
}