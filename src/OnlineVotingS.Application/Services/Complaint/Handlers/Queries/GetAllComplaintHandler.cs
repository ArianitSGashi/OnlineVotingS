using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetAllComplaintHandler : IRequestHandler<GetAllComplaintCommand, IEnumerable<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllComplaintHandler> _logger;

    public GetAllComplaintHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<GetAllComplaintHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<Complaints>> Handle(GetAllComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _complaintRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the complaints: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}