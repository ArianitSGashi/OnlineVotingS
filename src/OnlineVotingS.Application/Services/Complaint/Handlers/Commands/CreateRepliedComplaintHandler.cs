using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands
{
    public class CreateRepliedComplaintHandler : IRequestHandler<CreateRepliedComplaintCommand, RepliedComplaints>
    {
        private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
        private readonly IComplaintRepository _complaintRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRepliedComplaintHandler> _logger;

        public CreateRepliedComplaintHandler(
            IRepliedComplaintsRepository repliedComplaintsRepository,
            IComplaintRepository complaintRepository,
            IMapper mapper,
            ILogger<CreateRepliedComplaintHandler> logger)
        {
            _repliedComplaintsRepository = repliedComplaintsRepository;
            _complaintRepository = complaintRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RepliedComplaints> Handle(CreateRepliedComplaintCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Verify that the related complaint exists
                var complaint = await _complaintRepository.GetByIdAsync(request.RepliedComplaintsPostDTO.ComplaintID);
                if (complaint == null)
                {
                    throw new KeyNotFoundException($"Complaint with ID {request.RepliedComplaintsPostDTO.ComplaintID} not found.");
                }

                // Map DTO to entity
                var repliedComplaint = _mapper.Map<RepliedComplaints>(request.RepliedComplaintsPostDTO);

                // Add to repository
                await _repliedComplaintsRepository.AddAsync(repliedComplaint);

                return repliedComplaint;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating the replied complaint: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
