using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Commands;

public class UpdateRepliedComplaintCommandHandler : IRequestHandler<UpdateRepliedComplaintCommand, RepliedComplaints>
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

    public async Task<RepliedComplaints> Handle(UpdateRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaint = await _repliedComplaintsRepository.GetByIdAsync(request.RepliedComplaint.RepliedComplaintID);
            if (repliedComplaint == null)
            {
                throw new KeyNotFoundException($"Replied complaint with ID {request.RepliedComplaint.RepliedComplaintID} not found.");
            }

            _mapper.Map(request.RepliedComplaint, repliedComplaint);
            await _repliedComplaintsRepository.UpdateAsync(repliedComplaint);
            return repliedComplaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaint.RepliedComplaintID, ex.Message);
            throw;
        }
    }
}