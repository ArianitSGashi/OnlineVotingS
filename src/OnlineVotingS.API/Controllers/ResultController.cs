using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultController : ControllerBase
{
    private readonly ISender _mediator;

    public ResultController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Result>> Create([FromBody] ResultPostDTO resultPostDto)
    {
        var command = new CreateResultCommand(resultPostDto);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { resultId = result.ResultID }, result);
    }

    [HttpDelete("{resultId:int}")]
    public async Task<IActionResult> Delete(int resultId)
    {
        var command = new DeleteResultCommand(resultId);
        var result = await _mediator.Send(command);
        if (result)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<Result>> Update([FromBody] ResultPutDTO resultPutDto)
    {
        var command = new UpdateResultCommand(resultPutDto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Result>>> GetAll()
    {
        var query = new GetAllResultsQuery();
        var results = await _mediator.Send(query);
        return Ok(results);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<Result>> GetById([FromQuery] int resultId)
    {
        var query = new GetResultByIdQuery(resultId);
        var result = await _mediator.Send(query);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpGet("GetByCandidateId")]
    public async Task<ActionResult<IEnumerable<Result>>> GetByCandidateId([FromQuery] int candidateId)
    {
        var query = new GetResultsByCandidateIdQuery(candidateId);
        var results = await _mediator.Send(query);
        return Ok(results);
    }

    [HttpGet("GetByElectionId")]
    public async Task<ActionResult<IEnumerable<Result>>> GetByElectionId([FromQuery] int electionId)
    {
        var query = new GetResultsByElectionIdQuery(electionId);
        var results = await _mediator.Send(query);
        return Ok(results);
    }

    [HttpGet("GetByTotalVotesGreaterThan")]
    public async Task<ActionResult<IEnumerable<Result>>> GetByTotalVotesGreaterThan([FromQuery] int votes)
    {
        var query = new GetResultsByTotalVotesGreaterThanQuery(votes);
        var results = await _mediator.Send(query);
        return Ok(results);
    }
}
