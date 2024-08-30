using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintsController : Controller
{
    private readonly ISender Mediator;
    public ComplaintsController(ISender _mediator)
    {
        Mediator = _mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ComplaintsPostDTO complaintsPost)
    {
        var command = new CreateComplaintCommand(complaintsPost);
        var response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int ComplaintId)
    {
        var command = new DeleteComplaintCommand(ComplaintId);
        var response = await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ComplaintsPutDTO complaintsPut)
    {
        var command = new UpdateComplaintCommand(complaintsPut);
        var response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllComplaintCommand();
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("date/{Date}")]
    public async Task<IActionResult> GetAllByDate([FromQuery] DateTime Date)
    {
        var query = new GetByComplaintDateCommand(Date);
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("election/{ElectionId}")]
    public async Task<IActionResult> GetAllByElectionId([FromQuery] int ElectionId)
    {
        var query = new GetComplaintByElectionIdCommand(ElectionId);
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{ComplaintId}")]
    public async Task<IActionResult> GetById([FromQuery] int ComplaintId)
    {
        var query = new GetComplaintsByIdCommand(ComplaintId);
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("user/{UserId}")]
    public async Task<IActionResult> GetComplaintsByUserId([FromQuery] string UserId)
    {
        var query = new GetComplaintsByUserIdCommand(UserId);
        var response = await Mediator.Send(query);

        return Ok(response);
    }
}
