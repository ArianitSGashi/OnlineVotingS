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

    public class GetByComplaintIDQueryHandler : IRequestHandler<GetByComplaintIDQuery, IEnumerable<RepliedComplaints>>
    {
        private readonly IRepliedComplaintsRepository _repliedComplaintsRepository;
        private readonly ILogger<GetByComplaintIDQueryHandler> _logger;

        public GetByComplaintIDQueryHandler(IRepliedComplaintsRepository repliedComplaintsRepository, ILogger<GetByComplaintIDQueryHandler> logger)
        {
            _repliedComplaintsRepository = repliedComplaintsRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<RepliedComplaints>> Handle(GetByComplaintIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repliedComplaintsRepository.GetByComplaintIDAsync(request.ComplaintID);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving replied complaints for complaint ID {ComplaintID}: {ErrorMessage}", request.ComplaintID, ex.Message);
                throw;
            }
        }
    }