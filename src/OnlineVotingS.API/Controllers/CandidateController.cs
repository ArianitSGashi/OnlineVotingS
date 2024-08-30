using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;

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
    public async Task<IActionResult> CreateAsync([FromBody] CandidatesPostDTO candidatesPost)
    {
        var command = new CreateCandidateCommand(candidatesPost);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdAsync), new { candidateId = result.CandidateID }, result);
    }

    [HttpDelete("{candidateId:int}")]
    public async Task<IActionResult> DeleteAsync(int candidateId)
    {
        var command = new DeleteCandidateCommand(candidateId);
        var result = await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CandidatesPutDTO candidatesPut)
    {
        var command = new UpdateCandidateCommand(candidatesPut);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllCandidatesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{candidateId:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int candidateId)
    {
        var query = new GetCandidateByIdQuery(candidateId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("election/{electionId:int}")]
    public async Task<IActionResult> GetByElectionIdAsync([FromRoute] int electionId)
    {
        var query = new GetCandidatesByElectionIdQuery(electionId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("income/{minIncome:decimal}")]
    public async Task<IActionResult> GetByMinIncomeAsync([FromRoute] decimal minIncome)
    {
        var query = new GetCandidatesByMinIncomeQuery(minIncome);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
    {
        var query = new GetCandidatesByNameQuery(name);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("party/{party}")]
    public async Task<IActionResult> GetByPartyAsync([FromRoute] string party)
    {
        var query = new GetCandidatesByPartyQuery(party);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
