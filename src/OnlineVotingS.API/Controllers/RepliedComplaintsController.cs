using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepliedComplaintsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RepliedComplaintsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RepliedComplaints>>> GetAllRepliedComplaints()
    {
        var query = new GetAllRepliedComplaintsQuery();
        var repliedComplaints = await _mediator.Send(query);
        return Ok(repliedComplaints);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RepliedComplaints>> GetRepliedComplaintById(int id)
    {
        var query = new GetRepliedComplaintByIdQuery(id);
        var repliedComplaint = await _mediator.Send(query);
        return repliedComplaint != null ? Ok(repliedComplaint) : NotFound($"Replied complaint with ID {id} not found.");
    }

    [HttpGet("complaint/{complaintId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RepliedComplaints>>> GetRepliedComplaintsByComplaintId(int complaintId)
    {
        var query = new GetRepliedComplaintsByComplaintIDQuery(complaintId);
        var repliedComplaints = await _mediator.Send(query);
        return Ok(repliedComplaints);
    }

    [HttpGet("replytext/{replyText}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RepliedComplaints>>> GetRepliedComplaintsByReplyText(string replyText)
    {
        var query = new GetRepliedComplaintsByReplyTextQuery(replyText);
        var repliedComplaints = await _mediator.Send(query);
        return Ok(repliedComplaints);
    }

    [HttpGet("recent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RepliedComplaints>>> GetRecentRepliedComplaints([FromQuery] DateTime date)
    {
        var query = new GetRecentRepliedComplaintsQuery(date);
        var repliedComplaints = await _mediator.Send(query);
        return Ok(repliedComplaints);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<RepliedComplaints>> CreateRepliedComplaint([FromBody] RepliedComplaintsPostDTO repliedComplaintDto)
    {
        var command = new CreateRepliedComplaintCommand(repliedComplaintDto);
        var repliedComplaint = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRepliedComplaintById), new { id = repliedComplaint.RepliedComplaintID }, repliedComplaint);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRepliedComplaint([FromBody] RepliedComplaintsPutDTO repliedComplaintDto)
    {
        if (repliedComplaintDto == null || repliedComplaintDto.RepliedComplaintID == 0)
        {
            return BadRequest("Replied Complaint ID is required.");
        }
        var command = new UpdateRepliedComplaintCommand(repliedComplaintDto);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRepliedComplaint(int id)
    {
        var command = new DeleteRepliedComplaintCommand(id);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound($"Replied complaint with ID {id} not found.");
    }
}