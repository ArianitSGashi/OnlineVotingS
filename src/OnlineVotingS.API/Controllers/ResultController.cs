using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Application.Services.Results.Requests.Queries;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllResultsAsync()
    {
        var query = new GetAllResultsQuery();
        var results = await _mediator.Send(query);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetResultByIdAsync(int id)
    {
        var query = new GetResultByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("candidate/{candidateId}")]
    public async Task<IActionResult> GetResultsByCandidateIdAsync(int candidateId)
    {
        var query = new GetResultsByCandidateIdQuery(candidateId);
        var results = await _mediator.Send(query);
        return Ok(results);
    }

    [HttpGet("election/{electionId}")]
    public async Task<IActionResult> GetResultsByElectionIdAsync(int electionId)
    {
        var query = new GetResultsByElectionIdQuery(electionId);
        var results = await _mediator.Send(query);
        return Ok(results);
    }

    [HttpGet("totalvotes/{votes}")]
    public async Task<IActionResult> GetResultsByTotalVotesGreaterThanAsync(int votes)
    {
        var query = new GetResultsByTotalVotesGreaterThanQuery(votes);
        var results = await _mediator.Send(query);
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> CreateResultAsync([FromBody] ResultPostDTO resultDto)
    {
        var command = new CreateResultCommand(resultDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var createdResult = result.Value;
            return CreatedAtAction(nameof(GetResultByIdAsync), new { id = createdResult.ResultID }, createdResult);
        }
        return BadRequest(result.Errors);  
    }


    [HttpPut]
    public async Task<IActionResult> UpdateResultAsync([FromBody] ResultPutDTO resultDto)
    {
        var command = new UpdateResultCommand(resultDto);
        var updatedResult = await _mediator.Send(command);
        return Ok(updatedResult);
    }

    [HttpDelete("{id?}")]
    public async Task<IActionResult> DeleteResultAsync(int id)
    {
        var command = new DeleteResultCommand(id);
        var result = await _mediator.Send(command);
        return NoContent();
    }
}