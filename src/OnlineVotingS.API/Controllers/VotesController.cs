using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Votes>>> GetAllVotes()
    {
        var query = new GetAllVotesQuery();
        var votes = await _mediator.Send(query);
        return Ok(votes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Votes>> GetVoteById(int id)
    {
        var query = new GetVoteByIdQuery(id);
        var vote = await _mediator.Send(query);
        return vote != null ? Ok(vote) : NotFound("Vote not found.");
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Votes>>> GetVotesByUserId(string userId)
    {
        var query = new GetVotesByUserIDQuery(userId);
        var votes = await _mediator.Send(query);
        return Ok(votes);
    }

    [HttpGet("election/{electionId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Votes>>> GetVotesByElectionId(int electionId)
    {
        var query = new GetVotesByElectionIDQuery(electionId);
        var votes = await _mediator.Send(query);
        return Ok(votes);
    }

    [HttpGet("candidate/{candidateId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Votes>>> GetVotesByCandidateId(int candidateId)
    {
        var query = new GetVotesByCandidateIDQuery(candidateId);
        var votes = await _mediator.Send(query);
        return Ok(votes);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Votes>> CreateVote([FromBody] VotesPostDTO voteDto)
    {
        var command = new CreateVoteCommand(voteDto);
        var vote = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetVoteById), new { id = vote.VoteID }, vote);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateVote([FromBody] VotesPutDTO voteDto)
    {
        if (voteDto == null || voteDto.VoteID == 0)
        {
            return BadRequest("Vote ID is required.");
        }

        var command = new UpdateVoteCommand(voteDto);
        var updatedVote = await _mediator.Send(command);
        return updatedVote != null ? Ok(updatedVote) : NotFound($"Vote with ID {voteDto.VoteID} not found.");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVote(int id)
    {
        var command = new DeleteVoteCommand(id);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }
}