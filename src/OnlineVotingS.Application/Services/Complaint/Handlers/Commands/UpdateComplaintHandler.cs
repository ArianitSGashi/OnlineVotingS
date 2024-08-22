using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.ImplService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class UpdateComplaintHandler : IRequestHandler<UpdateComplaintCommand, Complaints>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public UpdateComplaintHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Complaints> Handle(UpdateComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaint = await _complaintRepository.GetByIdAsync(request.ComplaintsPutDTO.ComplaintID);
            if (complaint == null)
            {
               throw new KeyNotFoundException($"Complaint with ID : {request.ComplaintsPutDTO.ComplaintID} not found.");
            }

            _mapper.Map(request.ComplaintsPutDTO, complaint);
            await _complaintRepository.UpdateAsync(complaint);
            
            return complaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating the complaint with ComplaintID: {ComplaintId}: {ErrorMessage}", request.ComplaintsPutDTO.ComplaintID, ex.Message);
            throw;
        }
    }
}
