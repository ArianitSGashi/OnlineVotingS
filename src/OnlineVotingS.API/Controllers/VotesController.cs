using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVotesAsync()
    {
        var query = new GetAllVotesQuery();
        var votes = await _mediator.Send(query);
        return Ok(votes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVoteByIdAsync(int id)
    {
        var query = new GetVoteByIdQuery(id);
        var vote = await _mediator.Send(query);
        return Ok(vote);
    }

    //[HttpGet("user/{userId?}")]
    //public async Task<IActionResult> GetVotesByUserIdAsync(string userId)
    //{
    //    var query = new GetVotesByUserIDQuery(userId);
    //    var votes = await _mediator.Send(query);
    //    return Ok(votes);
    //}

    [HttpGet("election/{electionId}")]
    public async Task<IActionResult> GetVotesByElectionIdAsync(int electionId)
    {
        var query = new GetVotesByElectionIDQuery(electionId);
        var votes = await _mediator.Send(query);
        return Ok(votes);
    }

    [HttpGet("candidate/{candidateId}")]
    public async Task<IActionResult> GetVotesByCandidateIdAsync(int candidateId)
    {
        var query = new GetVotesByCandidateIDQuery(candidateId);
        var votes = await _mediator.Send(query);
        return Ok(votes);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentVotesAsync([FromQuery] DateTime date)
    {
        var query = new GetRecentVotesQuery(date);
        var votes = await _mediator.Send(query);
        return Ok(votes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVoteAsync([FromBody] VotesPostDTO voteDto)
    {
        var command = new CreateVoteCommand(voteDto);
        var vote = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetVoteByIdAsync), new { id = vote.VoteID }, vote);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVoteAsync([FromBody] VotesPutDTO voteDto)
    {
        var command = new UpdateVoteCommand(voteDto);
        var updatedVote = await _mediator.Send(command);
        return Ok(updatedVote);
    }

    [HttpDelete("{id?}")]
    public async Task<IActionResult> DeleteVoteAsync(int id)
    {
        var command = new DeleteVoteCommand(id);
        var result = await _mediator.Send(command);
        return NoContent();
    }
}