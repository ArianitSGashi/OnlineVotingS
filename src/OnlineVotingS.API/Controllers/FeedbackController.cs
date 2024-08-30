using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Commands;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

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
    public async Task<IActionResult> GetAllFeedbacksAsync()
    {
        var query = new GetAllFeedbacksQuery();
        var feedbacks = await _mediator.Send(query);
        return Ok(feedbacks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeedbackByIdAsync(int id)
    {
        var query = new GetFeedbackByIdQuery(id);
        var feedback = await _mediator.Send(query);
        return Ok(feedback);
    }

    [HttpGet("user/{userId?}")]
    public async Task<IActionResult> GetFeedbacksByUserIdAsync(string userId)
    {
        var query = new GetFeedbacksByUserIdQuery(userId);
        var feedbacks = await _mediator.Send(query);
        return Ok(feedbacks);
    }

    [HttpGet("election/{electionId}")]
    public async Task<IActionResult> GetFeedbacksByElectionIdAsync(int electionId)
    {
        var query = new GetFeedbacksByElectionIdQuery(electionId);
        var feedbacks = await _mediator.Send(query);
        return Ok(feedbacks);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentFeedbacksAsync([FromQuery] DateTime date)
    {
        var query = new GetRecentFeedbacksQuery(date);
        var feedbacks = await _mediator.Send(query);
        return Ok(feedbacks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeedbackAsync([FromBody] FeedbackPostDTO feedbackDto)
    {
        var command = new CreateFeedbackCommand(feedbackDto);
        var feedback = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetFeedbackByIdAsync), new { id = feedback.FeedbackID }, feedback);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeedbackAsync([FromBody] FeedbackPutDTO feedbackDto)
    {
        var command = new UpdateFeedbackCommand(feedbackDto);
        var updatedFeedback = await _mediator.Send(command);
        return Ok(updatedFeedback);
    }

    [HttpDelete("{id?}")]
    public async Task<IActionResult> DeleteFeedbackAsync(int id)
    {
        var command = new DeleteFeedbackCommand(id);
        var result = await _mediator.Send(command);
        return NoContent();
    }
}