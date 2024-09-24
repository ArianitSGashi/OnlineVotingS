using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetByComplaintDateHandler : IRequestHandler<GetByComplaintDateCommand, Result<IEnumerable<Complaints>>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<GetByComplaintDateHandler> _logger;

    public GetByComplaintDateHandler(IComplaintRepository complaintRepository, ILogger<GetByComplaintDateHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Complaints>>> Handle(GetByComplaintDateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaints = await _complaintRepository.GetByComplaintDateAsync(request.Date);
            return Ok(complaints);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching complaints with Date {Date}: {ErrorMessage}", request.Date.ToString(), ex.Message);
            return new Result<IEnumerable<Complaints>>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
        }
    }
}