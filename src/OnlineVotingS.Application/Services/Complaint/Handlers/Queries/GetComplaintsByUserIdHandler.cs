using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Application.Services.ImplService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetComplaintsByUserIdHandler : IRequestHandler<GetComplaintsByUserIdCommand, IEnumerable<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public GetComplaintsByUserIdHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
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
