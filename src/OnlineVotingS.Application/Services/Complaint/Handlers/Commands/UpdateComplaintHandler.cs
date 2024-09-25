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

public class UpdateComplaintHandler : IRequestHandler<UpdateComplaintCommand, Result<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateComplaintHandler> _logger;

    public UpdateComplaintHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<UpdateComplaintHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<Complaints>> Handle(UpdateComplaintCommand request, CancellationToken cancellationToken)
    {
        var complaint = await _complaintRepository.GetByIdAsync(request.ComplaintsPutDTO.ComplaintID);
        if (complaint == null)
        {
            return new Result<Complaints>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
        }

        try
        {
            _mapper.Map(request.ComplaintsPutDTO, complaint);
            await _complaintRepository.UpdateAsync(complaint);
            return Ok(complaint);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the complaint with ComplaintID: {ComplaintId}: {ErrorMessage}", request.ComplaintsPutDTO.ComplaintID, ex.Message);
            return new Result<Complaints>().WithError(ErrorCodes.COMPLAIN_UPDATE_FAILED.ToString());
        }
    }
}
