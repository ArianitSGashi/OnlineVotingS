using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComplaintsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllComplaintsAsync()
    {
        var query = new GetAllComplaintCommand();
        var complaints = await _mediator.Send(query);
        return Ok(complaints);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComplaintByIdAsync(int id)
    {
        var query = new GetComplaintsByIdCommand(id);
        var complaint = await _mediator.Send(query);
        return Ok(complaint);
    }

    [HttpGet("user/{userId?}")]
    public async Task<IActionResult> GetComplaintsByUserIdAsync(string userId)
    {
        var query = new GetComplaintsByUserIdCommand(userId);
        var complaints = await _mediator.Send(query);
        return Ok(complaints);
    }

    [HttpGet("election/{electionId}")]
    public async Task<IActionResult> GetComplaintsByElectionIdAsync(int electionId)
    {
        var query = new GetComplaintByElectionIdCommand(electionId);
        var complaints = await _mediator.Send(query);
        return Ok(complaints);
    }

    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetComplaintsByDateAsync(DateTime date)
    {
        var query = new GetByComplaintDateCommand(date);
        var complaints = await _mediator.Send(query);
        return Ok(complaints);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComplaintAsync([FromBody] ComplaintsPostDTO complaintDto)
    {
        var command = new CreateComplaintCommand(complaintDto);
        var complaint = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetComplaintByIdAsync), new { id = complaint.ComplaintID }, complaint);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComplaintAsync([FromBody] ComplaintsPutDTO complaintDto)
    {
        var command = new UpdateComplaintCommand(complaintDto);
        var updatedComplaint = await _mediator.Send(command);
        return Ok(updatedComplaint);
    }

    [HttpDelete("{id?}")]
    public async Task<IActionResult> DeleteComplaintAsync(int id)
    {
        var command = new DeleteComplaintCommand(id);
        var result = await _mediator.Send(command);
        return NoContent();
    }
    // New endpoint for replying to a complaint
    [HttpPost("Reply")]
    public async Task<IActionResult> ReplyComplaintAsync([FromBody] RepliedComplaintsPostDTO repliedComplaintDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new CreateRepliedComplaintCommand(repliedComplaintDto);
        try
        {
            var result = await _mediator.Send(command);
            return Ok("Reply sent successfully.");
        }
        catch (KeyNotFoundException knfEx)
        {
            return NotFound(knfEx.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}