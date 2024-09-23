using FluentResults;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.CostumExceptions;

namespace OnlineVotingS.Application.Services.Complaint.Handlers.Commands;

public class CreateComplaintHandler : IRequestHandler<CreateComplaintCommand, FluentResults.Result<Complaints>>
{
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateComplaintHandler> _logger;

    public CreateComplaintHandler(IComplaintRepository complaintRepository, IMapper mapper, ILogger<CreateComplaintHandler> logger)
    {
        _complaintRepository = complaintRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FluentResults.Result<Complaints>> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var complaint = _mapper.Map<Complaints>(request.ComplaintsPostDTO);
            await _complaintRepository.AddAsync(complaint);
            return FluentResults.Result.Ok(complaint);
        }
        catch (DbUpdateException ex) when (ex.InnerException != null)
        {
            _logger.LogError(ex, "A database error occurred while creating the complaint.");
            var errorMessage = "A database error occurred while saving the complaint. Please try again.";
            return FluentResults.Result.Fail(new ExceptionalError(new DbUpdateException(errorMessage, ex)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the complaint.");
            return FluentResults.Result.Fail(new ExceptionalError(ex));
        }
    }
}
