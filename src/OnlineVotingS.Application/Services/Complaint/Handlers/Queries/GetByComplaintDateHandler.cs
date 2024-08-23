using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetByComplaintDateHandler : IRequestHandler<GetByComplaintDateCommand, IEnumerable<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<GetByComplaintDateHandler> _logger;

    public GetByComplaintDateHandler(IComplaintRepository complaintRepository, ILogger<GetByComplaintDateHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Complaints>> Handle(GetByComplaintDateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _complaintRepository.GetByComplaintDateAsync(request.Date);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching campaigns with Date {Date}: {ErrorMessage}", request.Date.ToString(), ex.Message);
            throw;
        }
    }
}