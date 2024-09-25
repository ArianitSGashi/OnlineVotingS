using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using static FluentResults.Result;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Queries;

public class GetAllComplaintHandler : IRequestHandler<GetAllComplaintCommand, Result<IEnumerable<Complaints>>>
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

    public async Task<Result<IEnumerable<Complaints>>> Handle(GetAllComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaints = await _complaintRepository.GetAllAsync();
            return Ok(complaints);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while fetching the complaints: {ErrorMessage}", ex.Message);
            return new Result<IEnumerable<Complaints>>().WithError(ErrorCodes.COMPLAIN_NOT_FOUND.ToString());
        }
    }
}