using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetComplaintByElectionIdHandler : IRequestHandler<GetComplaintByElectionIdCommand, IEnumerable<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<GetComplaintByElectionIdHandler> _logger;

    public GetComplaintByElectionIdHandler(IComplaintRepository complaintRepository, ILogger<GetComplaintByElectionIdHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Complaints>> Handle(GetComplaintByElectionIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _complaintRepository.GetByElectionIdAsync(request.ElectionId);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching campaigns for ElectionID {ElectionId}: {ErrorMessage}", request.ElectionId, ex.Message);
            throw;
        }
    }
}