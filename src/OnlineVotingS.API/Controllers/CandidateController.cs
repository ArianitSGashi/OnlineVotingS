using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidateController : ControllerBase
{
    private readonly ISender _mediator;

    public CandidateController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Candidates>> Create([FromBody] CandidatesPostDTO candidatesPost)
    {
        var command = new CreateCandidateCommand(candidatesPost);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { candidateId = result.CandidateID }, result);
    }

    [HttpDelete("{candidateId}")]
    public async Task<IActionResult> Delete(int candidateId)
    {
        var command = new DeleteCandidateCommand(candidateId);
        var result = await _mediator.Send(command);
        if (result)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult<Candidates>> Update([FromBody] CandidatesPutDTO candidatesPut)
    {
        var command = new UpdateCandidateCommand(candidatesPut);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetAll()
    {
        var query = new GetAllCandidatesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<Candidates>> GetById([FromQuery] int candidateId)
    {
        var query = new GetCandidateByIdQuery(candidateId);
        var result = await _mediator.Send(query);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpGet("GetByElectionId")]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetByElectionId([FromQuery] int electionId)
    {
        var query = new GetCandidatesByElectionIdQuery(electionId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetByMinIncome")]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetByMinIncome([FromQuery] decimal minIncome)
    {
        var query = new GetCandidatesByMinIncomeQuery(minIncome);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetByName")]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetByName([FromQuery] string name)
    {
        var query = new GetCandidatesByNameQuery(name);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetByParty")]
    public async Task<ActionResult<IEnumerable<Candidates>>> GetByParty([FromQuery] string party)
    {
        var query = new GetCandidatesByPartyQuery(party);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
