using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Handlers.Queries;

    public class GetRepliedComplaintByIdQueryHandler : IRequestHandler<GetRepliedComplaintByIdQuery, RepliedComplaints>
    {
        private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
        private readonly ILogger<GetRepliedComplaintByIdQueryHandler> _logger;

        public GetRepliedComplaintByIdQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetRepliedComplaintByIdQueryHandler> logger)
        {
            _repliedComplaintsRepository = repliedComplaintsRepository;
            _logger = logger;
        }

        public async Task<RepliedComplaints> Handle(GetRepliedComplaintByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repliedComplaintsRepository.GetByIdAsync(request.RepliedComplaintId);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaintId, ex.Message);
                throw;
            }
        }
    }