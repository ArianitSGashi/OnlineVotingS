using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidateController : ControllerBase
{
    private readonly IMediator _mediator;

    public CandidateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Candidates>> Create([FromBody] CandidatesPostDTO candidatesPost)
    {
        if (candidatesPost == null)
        {
            return BadRequest("Candidate data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new CreateCandidateCommand(candidatesPost);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { candidateId = result.CandidateID }, result);
    }

    [HttpDelete("{candidateId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int candidateId)
    {
        var command = new DeleteCandidateCommand(candidateId);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound($"Candidate with ID {candidateId} not found.");
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Candidates>> Update([FromBody] CandidatesPutDTO candidatesPut)
    {
        if (candidatesPut == null || candidatesPut.CandidateID <= 0)
        {
            return BadRequest("Valid Candidate ID is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdateCandidateCommand(candidatesPut);
        var result = await _mediator.Send(command);

        return result == null
            ? NotFound($"Candidate with ID {candidatesPut.CandidateID} not found.")
            : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetAll()
    {
        var query = new GetAllCandidatesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{candidateId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Candidates>> GetById([FromRoute] int candidateId)
    {
        var query = new GetCandidateByIdQuery(candidateId);
        var result = await _mediator.Send(query);
        return result == null ? NotFound($"Candidate with ID {candidateId} not found.") : Ok(result);
    }

    [HttpGet("election/{electionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetByElectionId([FromRoute] int electionId)
    {
        var query = new GetCandidatesByElectionIdQuery(electionId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("income/{minIncome:decimal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetByMinIncome([FromRoute] decimal minIncome)
    {
        var query = new GetCandidatesByMinIncomeQuery(minIncome);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("name/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetByName([FromRoute] string name)
    {
        var query = new GetCandidatesByNameQuery(name);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("party/{party}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetByParty([FromRoute] string party)
    {
        var query = new GetCandidatesByPartyQuery(party);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
