using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeedbackController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedbacks()
    {
        var query = new GetAllFeedbacksQuery();
        var feedbacks = await _mediator.Send(query);
        return Ok(feedbacks);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Feedback>> GetFeedbackById(int id)
    {
        var query = new GetFeedbackByIdQuery(id);
        var feedback = await _mediator.Send(query);
        return feedback != null ? Ok(feedback) : NotFound("Feedback not found.");
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacksByUserId(string userId)
    {
        var query = new GetFeedbacksByUserIdQuery(userId);
        var feedbacks = await _mediator.Send(query);
        return Ok(feedbacks);
    }

    [HttpGet("election/{electionId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacksByElectionId(int electionId)
    {
        var query = new GetFeedbacksByElectionIdQuery(electionId);
        var feedbacks = await _mediator.Send(query);
        return Ok(feedbacks);
    }

    [HttpGet("recent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetRecentFeedbacks([FromQuery] DateTime date)
    {
        var query = new GetRecentFeedbacksQuery(date);
        var feedbacks = await _mediator.Send(query);
        return Ok(feedbacks);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Feedback>> CreateFeedback([FromBody] FeedbackPostDTO feedbackDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new CreateFeedbackCommand(feedbackDto);
        var feedback = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.FeedbackID }, feedback);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFeedback([FromBody] FeedbackPutDTO feedbackDto)
    {
        if (!ModelState.IsValid || feedbackDto.FeedbackID == 0)
        {
            return BadRequest("Feedback ID is required and must be valid.");
        }

        var command = new UpdateFeedbackCommand(feedbackDto);
        var updatedFeedback = await _mediator.Send(command);
        return updatedFeedback != null ? NoContent() : NotFound($"Feedback with ID {feedbackDto.FeedbackID} not found.");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFeedback(int id)
    {
        var command = new DeleteFeedbackCommand(id);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound($"Feedback with ID {id} not found.");
    }
}
