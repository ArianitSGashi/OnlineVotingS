using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;


namespace OnlineVotingS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepliedComplaintsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RepliedComplaintsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllRepliedComplaintsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetRepliedComplaintByIdQuery(id));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRepliedComplaintCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = createdId }, command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRepliedComplaintCommand command)
    {
        if (id != command.RepliedComplaint.RepliedComplaintID) 
        {
            return BadRequest("ID in URL and model do not match.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteRepliedComplaintCommand(id));

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}