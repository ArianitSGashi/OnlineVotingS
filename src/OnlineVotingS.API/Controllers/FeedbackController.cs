﻿using MediatR;
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
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var feedback = result.Value;
            return CreatedAtAction(nameof(GetFeedbackByIdAsync), new { id = feedback.FeedbackID }, feedback);
        }
        return BadRequest(result.Errors); 
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