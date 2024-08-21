using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.ImplService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.GetComplaintByElectionId;

public class GetComplaintByElectionIdCommandHandler : IRequestHandler<GetComplaintByElectionIdCommand, IEnumerable<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public GetComplaintByElectionIdCommandHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
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
            _logger.LogError($"An error occurred while fetching the complaints with ElectionID {request.ElectionId}: {ex.Message}");
            throw;
        }
    }
}
