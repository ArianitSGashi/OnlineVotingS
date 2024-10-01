using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Candidate.Handlers.Queries;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Domain.Errors;
using OnlineVotingS.Domain.Interfaces; // Ensure you have this to access ICandidateRepository
using OnlineVotingS.Application; // Include this to access Result<T>

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
    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var query = new GetPaginatedCandidatesQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);

        return Ok(new
        {
            Candidates = result.Items,
            result.PageIndex,
            result.TotalPages,
            result.HasPreviousPage,
            result.HasNextPage
        });
    }



    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllCandidatesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{candidateId}")]
    public async Task<IActionResult> GetByIdAsync(int candidateId)
    {
        var query = new GetCandidateByIdQuery(candidateId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("election/{electionId}")]
    public async Task<IActionResult> GetByElectionIdAsync(int electionId)
    {
        var query = new GetCandidatesByElectionIdQuery(electionId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("income/{minIncome:decimal}")]
    public async Task<IActionResult> GetByMinIncomeAsync(decimal minIncome)
    {
        var query = new GetCandidatesByMinIncomeQuery(minIncome);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name)
    {
        var query = new GetCandidatesByNameQuery(name);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("party/{party}")]
    public async Task<IActionResult> GetByPartyAsync(string party)
    {
        var query = new GetCandidatesByPartyQuery(party);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CandidatesPostDTO candidatesPost)
    {
        var command = new CreateCandidateCommand(candidatesPost);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var candidate = result.Value;
            return CreatedAtAction(nameof(GetByIdAsync), new { candidateId = candidate.CandidateID }, candidate);
        }

        return BadRequest(result.Errors); 
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CandidatesPutDTO candidatesPut)
    {
        var command = new UpdateCandidateCommand(candidatesPut);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{candidateId?}")]
    public async Task<IActionResult> DeleteAsync(int candidateId)
    {
        var command = new DeleteCandidateCommand(candidateId);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Errors.FirstOrDefault();
        if (error != null)
        {
            switch (error.Message)
            {
                case nameof(ErrorCodes.CANDIDATE_NOT_FOUND):
                    return NotFound(new { message = "Candidate not found", candidateId });
                case nameof(ErrorCodes.CANDIDATE_DELETION_FAILED):
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { message = "Failed to delete candidate", candidateId });
                default:
                    return BadRequest(new { message = error.Message, candidateId });
            }
        }

        return BadRequest(new { message = "An unknown error occurred", candidateId });
    }
}
