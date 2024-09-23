using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

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
    public async Task<IActionResult> GetAllRepliedComplaintsAsync()
    {
        var query = new GetAllRepliedComplaintsQuery();
        var repliedComplaints = await _mediator.Send(query);
        return Ok(repliedComplaints);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRepliedComplaintByIdAsync(int id)
    {
        var query = new GetRepliedComplaintByIdQuery(id);
        var repliedComplaint = await _mediator.Send(query);
        return Ok(repliedComplaint);
    }

    [HttpGet("complaint/{complaintId}")]
    public async Task<IActionResult> GetRepliedComplaintsByComplaintIdAsync(int complaintId)
    {
        var query = new GetRepliedComplaintsByComplaintIDQuery(complaintId);
        var repliedComplaints = await _mediator.Send(query);
        return Ok(repliedComplaints);
    }

    [HttpGet("replytext/{replyText}")]
    public async Task<IActionResult> GetRepliedComplaintsByReplyTextAsync(string replyText)
    {
        var query = new GetRepliedComplaintsByReplyTextQuery(replyText);
        var repliedComplaints = await _mediator.Send(query);
        return Ok(repliedComplaints);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentRepliedComplaintsAsync([FromQuery] DateTime date)
    {
        var query = new GetRecentRepliedComplaintsQuery(date);
        var repliedComplaints = await _mediator.Send(query);
        return Ok(repliedComplaints);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRepliedComplaintAsync([FromBody] RepliedComplaintsPostDTO repliedComplaintDto)
    {
        var command = new CreateRepliedComplaintCommand(repliedComplaintDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var repliedComplaint = result.Value;
            return CreatedAtAction(nameof(GetRepliedComplaintByIdAsync), new { id = repliedComplaint.RepliedComplaintID }, repliedComplaint);
        }
        return BadRequest(result.Errors);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRepliedComplaintAsync([FromBody] RepliedComplaintsPutDTO repliedComplaintDto)
    { 
        var command = new UpdateRepliedComplaintCommand(repliedComplaintDto);
        var updatedRepliedComplaint = await _mediator.Send(command);
        return Ok(updatedRepliedComplaint);
    }

    [HttpDelete("{id?}")]
    public async Task<IActionResult> DeleteRepliedComplaintAsync(int id)
    {
        var command = new DeleteRepliedComplaintCommand(id);
        var result = await _mediator.Send(command);
        return NoContent();
    }
}