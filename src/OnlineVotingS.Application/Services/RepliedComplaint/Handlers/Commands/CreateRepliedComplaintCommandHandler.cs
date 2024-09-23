using static FluentResults.Result;
using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Commands;

public class CreateRepliedComplaintCommandHandler : IRequestHandler<CreateRepliedComplaintCommand, Result<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRepliedComplaintCommandHandler> _logger;

    public CreateRepliedComplaintCommandHandler(IRepliedComplaintsRepository repliedComplaintsRepository, IMapper mapper, ILogger<CreateRepliedComplaintCommandHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<RepliedComplaints>> Handle(CreateRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaint = _mapper.Map<RepliedComplaints>(request.RepliedComplaint);
            await _repliedComplaintsRepository.AddAsync(repliedComplaint);
            return Ok(repliedComplaint);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a replied complaint: {ErrorMessage}", ex.Message);
            return new Result<RepliedComplaints>().WithError(ErrorCodes.FEEDBACK_CREATION_FAILED.ToString());
        }
    }
}