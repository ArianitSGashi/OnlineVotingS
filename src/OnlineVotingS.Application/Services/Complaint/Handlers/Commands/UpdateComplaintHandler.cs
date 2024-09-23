using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.CostumExceptions;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class UpdateComplaintHandler : IRequestHandler<UpdateComplaintCommand, FluentResults.Result<Complaints>>
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

    public async Task<FluentResults.Result<Complaints>> Handle(UpdateComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaint = await _complaintRepository.GetByIdAsync(request.ComplaintsPutDTO.ComplaintID);
            if (complaint == null)
            {
                var errorMessage = $"Complaint with ID: {request.ComplaintsPutDTO.ComplaintID} not found.";
                return FluentResults.Result.Fail(new ExceptionalError(new KeyNotFoundException(errorMessage)));
            }

            _mapper.Map(request.ComplaintsPutDTO, complaint);
            await _complaintRepository.UpdateAsync(complaint);
            return FluentResults.Result.Ok(complaint);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the complaint with ComplaintID: {ComplaintId}: {ErrorMessage}", request.ComplaintsPutDTO.ComplaintID, ex.Message);
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
