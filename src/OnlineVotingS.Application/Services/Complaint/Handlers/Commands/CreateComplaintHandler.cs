using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.ImplService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class CreateComplaintHandler : IRequestHandler<CreateComplaintCommand, Complaints>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public CreateComplaintHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Complaints> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaint = _mapper.Map<Complaints>(request.ComplaintsPostDTO);
            await _complaintRepository.AddAsync(complaint);

            return complaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating the complaint: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}
