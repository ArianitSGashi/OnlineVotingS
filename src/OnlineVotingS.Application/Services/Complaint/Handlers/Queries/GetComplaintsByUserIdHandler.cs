using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetComplaintsByUserIdHandler : IRequestHandler<GetComplaintsByUserIdCommand, IEnumerable<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly ILogger<GetComplaintsByUserIdHandler> _logger;

    public GetComplaintsByUserIdHandler(IComplaintRepository complaintRepository, ILogger<GetComplaintsByUserIdHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Complaints>> Handle(GetComplaintsByUserIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _complaintRepository.GetByUserIdAsync(request.UserId);
        }
        catch (Exception ex)
        {

            _logger.LogError("An error occurred while fetching campaigns for UserId: {UserId}: {ErrorMessage}", request.UserId, ex.Message); 
            throw;
        }
    }
}