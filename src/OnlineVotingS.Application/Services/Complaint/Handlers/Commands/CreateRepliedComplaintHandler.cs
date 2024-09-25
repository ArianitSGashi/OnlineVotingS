using FluentResults;
using static FluentResults.Result;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class CreateRepliedComplaintHandler : IRequestHandler<CreateRepliedComplaintCommand, Result<RepliedComplaints>>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRepliedComplaintHandler> _logger;

    public CreateRepliedComplaintHandler(
        IRepliedComplaintsRepository repliedComplaintsRepository,
        IComplaintRepository complaintRepository,
        IMapper mapper,
        ILogger<CreateRepliedComplaintHandler> logger)
    {
        _repliedComplaintsRepository = repliedComplaintsRepository;
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<RepliedComplaints>> Handle(CreateRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        var complaint = await _complaintRepository.GetByIdAsync(request.RepliedComplaintsPostDTO.ComplaintID);
        if (complaint == null)
        {
            return new Result<RepliedComplaints>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
        }

        try
        {
            var repliedComplaint = _mapper.Map<RepliedComplaints>(request.RepliedComplaintsPostDTO);
            await _repliedComplaintsRepository.AddAsync(repliedComplaint);
            return Ok(repliedComplaint);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the replied complaint.");
            return new Result<RepliedComplaints>().WithError(ErrorCodes.REPLIED_COMPLAINT_CREATION_FAILED.ToString());
        }
    }
}
