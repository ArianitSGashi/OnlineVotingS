using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.RepliedComplaints.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.Application.Services.RepliedComplaints.Handlers.Queries;

public class GetRepliedComplaintByIdHandler : IRequestHandler<GetRepliedComplaintByIdQuery, RepliedComplaint>
{
    private readonly IRepliedComplaintsRepository _repliedComplaintRepository;
    private readonly ILogger<GetRepliedComplaintByIdHandler> _logger;

    public GetRepliedComplaintByIdHandler(IRepliedComplaintsRepository repliedComplaintRepository, ILogger<GetRepliedComplaintByIdHandler> logger)
    {
        _repliedComplaintRepository = repliedComplaintRepository;
        _logger = logger;
    }

    public async Task<RepliedComplaint> Handle(GetRepliedComplaintByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var repliedComplaint = await _repliedComplaintRepository.GetByIdAsync(request.RepliedComplaintsID);
            if (repliedComplaint == null)
            {
                _logger.LogWarning("Replied complaint with ID {RepliedComplaintId} not found.", request.RepliedComplaintsID);
                throw new KeyNotFoundException($"Replied complaint with ID {request.RepliedComplaintsID} not found.");
            }

            return repliedComplaint;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the replied complaint with ID {RepliedComplaintId}: {ErrorMessage}", request.RepliedComplaintsID, ex.Message);
            throw;
        }
    }
}