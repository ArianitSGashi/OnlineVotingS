using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetComplaintsByIdHandler : IRequestHandler<GetComplaintsByIdCommand, Result<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<GetComplaintsByIdHandler> _logger;

    public GetComplaintsByIdHandler(IComplaintRepository complaintRepository, ILogger<GetComplaintsByIdHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<Result<Complaints>> Handle(GetComplaintsByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaint = await _complaintRepository.GetByIdAsync(request.ComplaintId);
            if (complaint == null)
            {
                return new Result<Complaints>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
            }
            return Ok(complaint);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching complaint with ComplaintId: {ComplaintId}: {ErrorMessage}", request.ComplaintId, ex.Message);
            return new Result<Complaints>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
        }
    }
}