using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers;

public class ComplaintTestController : Controller
{
    private readonly ISender Mediator;
    public ComplaintTestController(ISender _mediator)
    {
        Mediator = _mediator;
    }

    [HttpPost]
    public async Task<Complaints> Create([FromBody] ComplaintsPostDTO complaintsPost)
    {
        var response = await Mediator.Send(new CreateComplaintCommand(complaintsPost));

        return response;
    }

    [HttpDelete]
    public async Task<bool> Delete([FromQuery] int ComplaintId)
    {
        var response = await Mediator.Send(new DeleteComplaintCommand(ComplaintId));

        return response;
    }

    [HttpPut]
    public async Task<Complaints> Update([FromBody] ComplaintsPutDTO complaintsPut)
    {
        var response = await Mediator.Send(new UpdateComplaintCommand(complaintsPut));

        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaints>> GetAll()
    {
        var response = await Mediator.Send(new GetAllComplaintCommand());

        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaints>> GetAllByDate([FromQuery] DateTime Date)
    {
        var response = await Mediator.Send(new GetByComplaintDateCommand(Date));

        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaints>> GetAllByElectionId([FromQuery] int ElectionId)
    {
        var response = await Mediator.Send(new GetComplaintByElectionIdCommand(ElectionId));

        return response;
    }

    [HttpGet]
    public async Task<Complaints> GetById([FromQuery] int ComplaintId)
    {
        var response = await Mediator.Send(new GetComplaintsByIdCommand(ComplaintId));

        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaints>> GetComplaintsByUserId([FromQuery] string UserId)
    {
        var response = await Mediator.Send(new GetComplaintsByUserIdCommand(UserId));

        return response;
    }
}
