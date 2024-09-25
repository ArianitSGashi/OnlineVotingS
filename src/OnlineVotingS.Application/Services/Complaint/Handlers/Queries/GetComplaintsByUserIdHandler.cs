using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetComplaintsByUserIdHandler : IRequestHandler<GetComplaintsByUserIdCommand, Result<IEnumerable<Complaints>>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<GetComplaintsByUserIdHandler> _logger;

    public GetComplaintsByUserIdHandler(IComplaintRepository complaintRepository, ILogger<GetComplaintsByUserIdHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Complaints>>> Handle(GetComplaintsByUserIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaints = await _complaintRepository.GetByUserIdAsync(request.UserId);
            return Ok(complaints);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching complaints for UserId: {UserId}: {ErrorMessage}", request.UserId, ex.Message);
            return new Result<IEnumerable<Complaints>>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
        }
    }
}