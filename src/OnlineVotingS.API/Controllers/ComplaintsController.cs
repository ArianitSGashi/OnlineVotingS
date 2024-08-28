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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Complaints>> Create([FromBody] ComplaintsPostDTO complaintsPost)
    {
        if (complaintsPost == null)
        {
            return BadRequest("Complaint data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new CreateComplaintCommand(complaintsPost);
        var response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetComplaintsByIdCommand), new { complaintText = complaintsPost.ComplaintText}, response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromQuery] int ComplaintId)
    {
        var command = new DeleteComplaintCommand(ComplaintId);
        var response = await Mediator.Send(command);

        return response
                  ? NoContent()
                  : NotFound($"Complaint with ID {ComplaintId} not found.");
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] ComplaintsPutDTO complaintsPut)
    {
        if (complaintsPut == null || complaintsPut.ComplaintID <= 0)
        {
            return BadRequest("Valid Complaint ID is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdateComplaintCommand(complaintsPut);
        var response = await Mediator.Send(command);

        return response == null
           ? NotFound($"Complaint with ID {response.ComplaintID} not found.")
           : NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Complaints>>> GetAll()
    {
        var query = new GetAllComplaintCommand();
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("date/{Date:datetime}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Complaints>>> GetAllByDate([FromQuery] DateTime Date)
    {
        var query = new GetByComplaintDateCommand(Date);
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("election/{ElectionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Complaints>>> GetAllByElectionId([FromQuery] int ElectionId)
    {
        var query = new GetComplaintByElectionIdCommand(ElectionId);
        var response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{ComplaintId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Complaints>> GetById([FromQuery] int ComplaintId)
    {
        var query = new GetComplaintsByIdCommand(ComplaintId);
        var response = await Mediator.Send(query);

        return response == null
            ? NotFound($"Complaint with ID {ComplaintId} not found.")
            : Ok(response);
    }

    [HttpGet("user/{UserId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Complaints>>> GetComplaintsByUserId([FromQuery] string UserId)
    {
        var query = new GetComplaintsByUserIdCommand(UserId);
        var response = await Mediator.Send(query);

        return Ok(response);
    }
}
