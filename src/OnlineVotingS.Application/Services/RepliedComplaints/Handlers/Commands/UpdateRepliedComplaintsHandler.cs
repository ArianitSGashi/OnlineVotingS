using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaints.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Handlers.Commands;

public class UpdateRepliedComplaintHandler : IRequestHandler<UpdateRepliedComplaintCommand, RepliedComplaint>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateRepliedComplaintHandler> _logger;

    public UpdateRepliedComplaintHandler(IRepliedComplaintsRepository repliedComplaintRepository, IMapper mapper, ILogger<UpdateRepliedComplaintHandler> logger)
    {
        _repliedComplaintRepository = repliedComplaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<RepliedComplaints> Handle(UpdateRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaint = await _repliedComplaintRepository.GetByIdAsync(request.RepliedComplaintDto.RepliedComplaintID);
            if (repliedComplaint == null)
            {
                _logger.LogWarning("Replied complaint with ID {RepliedComplaintId} not found.", request.RepliedComplaintDto.RepliedComplaintID);
                throw new KeyNotFoundException($"Replied complaint with ID {request.RepliedComplaintDto.RepliedComplaintID} not found.");
            }

            _mapper.Map(request.RepliedComplaintDto, repliedComplaint);
            await _repliedComplaintRepository.UpdateAsync(repliedComplaint);

            _logger.LogInformation("Replied complaint with ID {RepliedComplaintId} updated successfully.", request.RepliedComplaintDto.RepliedComplaintID);
            return repliedComplaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaintDto.RepliedComplaintID, ex.Message);
            throw;
        }
    }
}