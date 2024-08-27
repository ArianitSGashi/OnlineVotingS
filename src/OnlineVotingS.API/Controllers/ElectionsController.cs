using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ElectionsController : ControllerBase
{
    private readonly ISender _mediator;

    public ElectionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Elections>> Create([FromBody] ElectionsPostDTO electionsPost)
    {
        var elections = new CreateElectionsCommand(electionsPost);
        var result = await _mediator.Send(elections);
        return CreatedAtAction(nameof(GetById), new { electionId = result.ElectionID }, result);
    }

    [HttpDelete("{electionId}")]
    public async Task<IActionResult> Delete(int electionId)
    {
        var command = new DeleteElectionsCommand(electionId);
        var result = await _mediator.Send(command);
        if (result)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<Elections>> Update([FromBody] ElectionsPutDTO electionsPut)
    {
        var command = new UpdateElectionsCommand(electionsPut);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Elections>>> GetAll()
    {
        var query = new GetAllElectionsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [HttpGet("GetActive")]
    public async Task<ActionResult<IEnumerable<Elections>>> GetActive()
    {
        var query = new GetActiveElectionsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<Elections>> GetById([FromQuery] int electionId)
    {
        var query = new GetElectionsByIdQuery(electionId);
        var result = await _mediator.Send(query);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpGet("GetByTitle")]
    public async Task<ActionResult<IEnumerable<Elections>>> GetByTitle([FromQuery] string title)
    {
        var query = new GetByTitleQuery(title);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetUpcoming")]
    public async Task<ActionResult<IEnumerable<Elections>>> GetUpcoming([FromQuery] DateTime startDate)
    {
        var query = new GetUpcomingElectionsQuery(startDate);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}