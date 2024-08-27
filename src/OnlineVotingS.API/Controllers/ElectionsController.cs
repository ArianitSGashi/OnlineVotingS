using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElectionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ElectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Elections>>> GetAllElections()
    {
        var query = new GetAllElectionsQuery();
        var elections = await _mediator.Send(query);
        return Ok(elections);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Elections>> GetElectionById(int id)
    {
        var query = new GetElectionsByIdQuery(id);
        var election = await _mediator.Send(query);
        return election != null ? Ok(election) : NotFound($"Election with ID {id} not found.");
    }

    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Elections>>> GetActiveElections()
    {
        var query = new GetActiveElectionsQuery();
        var elections = await _mediator.Send(query);
        return Ok(elections);
    }

    [HttpGet("title/{title}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Elections>>> GetElectionsByTitle(string title)
    {
        var query = new GetByTitleQuery(title);
        var elections = await _mediator.Send(query);
        return Ok(elections);
    }

    [HttpGet("upcoming")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Elections>>> GetUpcomingElections(DateTime date)
    {
        var query = new GetUpcomingElectionsQuery(date);
        var elections = await _mediator.Send(query);
        return Ok(elections);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Elections>> CreateElection([FromBody] ElectionsPostDTO electionDto)
    {
        var command = new CreateElectionsCommand(electionDto);
        var election = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetElectionById), new { id = election.ElectionID }, election);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateElection([FromBody] ElectionsPutDTO electionDto)
    {
        if (electionDto == null || electionDto.ElectionID == 0)
        {
            return BadRequest("Election ID is required.");
        }

        var command = new UpdateElectionsCommand(electionDto);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteElection(int id)
    {
        var command = new DeleteElectionsCommand(id);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound($"Election with ID {id} not found.");
    }
}