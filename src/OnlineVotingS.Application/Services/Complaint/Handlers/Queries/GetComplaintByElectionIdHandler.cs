using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetComplaintByElectionIdHandler : IRequestHandler<GetComplaintByElectionIdCommand, Result<IEnumerable<Complaints>>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<GetComplaintByElectionIdHandler> _logger;

    public GetComplaintByElectionIdHandler(IComplaintRepository complaintRepository, ILogger<GetComplaintByElectionIdHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Complaints>>> Handle(GetComplaintByElectionIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaints = await _complaintRepository.GetByElectionIdAsync(request.ElectionId);
            return Ok(complaints);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching complaints for ElectionID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            return new Result<IEnumerable<Complaints>>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
        }
    }
}