using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaints.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Handlers.Commands;

public class CreateRepliedComplaintsHandler : IRequestHandler<CreateRepliedComplaintCommand, RepliedComplaints>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRepliedComplaintsHandler> _logger;

    public CreateRepliedComplaintsHandler(IRepliedComplaintsRepository repliedComplaintsRepository, IMapper mapper, ILogger<CreateRepliedComplaintsHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<RepliedComplaints> Handle(CreateRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaint = _mapper.Map<RepliedComplaints>(request.RepliedComplaintDto);
            await _repliedComplaintsRepository.AddAsync(repliedComplaint);

            _logger.LogInformation("Replied complaint created successfully with ID {RepliedComplaintId}.", repliedComplaint.RepliedComplaintID);
            return repliedComplaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating a replied complaint: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}