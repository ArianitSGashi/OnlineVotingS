using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.CostumExceptions;
using Microsoft.EntityFrameworkCore;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class CreateRepliedComplaintHandler : IRequestHandler<CreateRepliedComplaintCommand, FluentResults.Result<RepliedComplaints>>
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

    public async Task<FluentResults.Result<RepliedComplaints>> Handle(CreateRepliedComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaint = await _complaintRepository.GetByIdAsync(request.RepliedComplaintsPostDTO.ComplaintID);
            if (complaint == null)
            {
                var errorMessage = $"Complaint with ID {request.RepliedComplaintsPostDTO.ComplaintID} not found.";
                return FluentResults.Result.Fail(new ExceptionalError(new KeyNotFoundException(errorMessage)));
            }

            var repliedComplaint = _mapper.Map<RepliedComplaints>(request.RepliedComplaintsPostDTO);
            await _repliedComplaintsRepository.AddAsync(repliedComplaint);
            return FluentResults.Result.Ok(repliedComplaint);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "A database error occurred while creating the replied complaint.");
            var errorMessage = "A database error occurred while saving the replied complaint. Please try again.";
            return FluentResults.Result.Fail(new ExceptionalError(new DbUpdateException(errorMessage, ex)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the replied complaint.");
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
