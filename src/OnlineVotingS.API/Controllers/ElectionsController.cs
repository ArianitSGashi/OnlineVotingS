using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Application.Services.Election.Requests.Queries;

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
    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedElectionsAsync(int pageNumber = 1, int pageSize = 10)
    {
        var query = new GetPaginatedElectionsQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(new
            {
                Elections = result.Value.Items,
                result.Value.PageIndex,
                result.Value.TotalPages,
                result.Value.HasPreviousPage,
                result.Value.HasNextPage
            });
        }

        return BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllElectionsAsync()
    {
        var query = new GetAllElectionsQuery();
        var election = await _mediator.Send(query);
        return Ok(election);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetElectionsByIdAsync(int id)
    {
        var query = new GetElectionsByIdQuery(id);
        var election = await _mediator.Send(query);
        return Ok(election);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveElectionsAsync()
    {
        var query = new GetActiveElectionsQuery();
        var election = await _mediator.Send(query);
        return Ok(election);
    }

    [HttpGet("title/{title?}")]
    public async Task<IActionResult> GetElectionsByTitleAsync(string title)
    {
        var query = new GetByTitleQuery(title);
        var election = await _mediator.Send(query);
        return Ok(election);
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingElectionsAsync([FromQuery] DateTime date)
    {
        var query = new GetUpcomingElectionsQuery(date);
        var election = await _mediator.Send(query);
        return Ok(election);
    }

    [HttpPost]
    public async Task<IActionResult> CreateElectionsActionAsync([FromBody] ElectionsPostDTO electionDto)
    {
        var command = new CreateElectionsCommand(electionDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var election = result.Value; 
            return CreatedAtAction(nameof(GetElectionsByIdAsync), new { id = election.ElectionID }, election);
        }
        return BadRequest(result.Errors);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateElectionsAsync([FromBody] ElectionsPutDTO electionDto)
    {
        var command = new UpdateElectionsCommand(electionDto);
        var updatedElection = await _mediator.Send(command);
        return Ok(updatedElection);
    }

    [HttpDelete("{id?}")]
    public async Task<IActionResult> DeleteElectionsAsync(int id)
    {
        var command = new DeleteElectionsCommand(id);
        var result = await _mediator.Send(command);
        return NoContent();
    }
}