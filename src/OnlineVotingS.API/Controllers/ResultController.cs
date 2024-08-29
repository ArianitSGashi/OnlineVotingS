using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineVotingS.API.Controllers
{
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Result>>> GetAllResults()
        {
            var query = new GetAllResultsQuery();
            var results = await _mediator.Send(query);
            return Ok(results);
        }
        [HttpGet("{resultId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result>> GetResultById([FromRoute] int resultId)
        {
            var query = new GetResultByIdQuery(resultId);
            var result = await _mediator.Send(query);
            return result == null
                ? NotFound($"Result with ID {resultId} not found.")
                : Ok(result);
        }

        [HttpGet("candidate/{candidateId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByCandidateId([FromRoute] int candidateId)
        {
            var query = new GetResultsByCandidateIdQuery(candidateId);
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("election/{electionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByElectionId([FromRoute] int electionId)
        {
            var query = new GetResultsByElectionIdQuery(electionId);
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("votes/greater-than/{votes:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByTotalVotesGreaterThan([FromRoute] int votes)
        {
            var query = new GetResultsByTotalVotesGreaterThanQuery(votes);
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Result>> CreateResult([FromBody] ResultPostDTO resultPostDto)
        {
            if (resultPostDto == null)
            {
                return BadRequest("Result data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateResultCommand(resultPostDto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetResultById), new { resultId = result.ResultID }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateResult([FromBody] ResultPutDTO resultPutDto)
        {
            if (resultPutDto == null || resultPutDto.ResultID <= 0)
            {
                return BadRequest("Valid Result ID is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateResultCommand(resultPutDto);
            var updatedResult = await _mediator.Send(command);

            return updatedResult == null
                ? NotFound($"Result with ID {resultPutDto.ResultID} not found.")
                : NoContent();
        }

        [HttpDelete("{resultId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteResult([FromRoute] int resultId)
        {
            var command = new DeleteResultCommand(resultId);
            var result = await _mediator.Send(command);
            return result
                ? NoContent()
                : NotFound($"Result with ID {resultId} not found.");
        }
    }
}
