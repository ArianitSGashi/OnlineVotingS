using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Commands;

public class UpdateRepliedComplaintCommandHandler : IRequestHandler<UpdateRepliedComplaintCommand, FluentResults.Result<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateRepliedComplaintCommandHandler> _logger;

    public UpdateRepliedComplaintCommandHandler(IRepliedComplaintsRepository repliedComplaintsRepository, IMapper mapper, ILogger<UpdateRepliedComplaintCommandHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FluentResults.Result<RepliedComplaints>> Handle(UpdateRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaint = await _repliedComplaintsRepository.GetByIdAsync(request.RepliedComplaint.RepliedComplaintID);
            if (repliedComplaint == null)
            {
                return FluentResults.Result.Fail($"Replied complaint with ID {request.RepliedComplaint.RepliedComplaintID} not found.");
            }

            _mapper.Map(request.RepliedComplaint, repliedComplaint);
            await _repliedComplaintsRepository.UpdateAsync(repliedComplaint);
            return FluentResults.Result.Ok(repliedComplaint); 
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaint.RepliedComplaintID, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex)); 
        }
    }
}
