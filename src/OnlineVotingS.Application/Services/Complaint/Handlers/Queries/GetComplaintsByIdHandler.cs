﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Application.Services.ImplService;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetComplaintsByIdHandler : IRequestHandler<GetComplaintsByIdCommand, Complaints>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ComplaintService> _logger;

    public GetComplaintsByIdHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<ComplaintService> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Complaints> Handle(GetComplaintsByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _complaintRepository.GetByIdAsync(request.ComplaintId);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching complaint with ComplaintId: {ComplaintId}: {ErrorMessage}", request.ComplaintId, ex.Message);
            throw;
        }
    }
}
