using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;

namespace OnlineVotingS.API.Controllers.TempControllers;

[ApiController]
[Route("[controller]")]
public class CandidateController : Controller
{
    private readonly IMediator _mediator;

    public CandidateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("AddCandidate")]
    public async Task<IActionResult> AddCandidate()
    {
        var query = new GetAllCandidatesQuery();
        var candidates = await _mediator.Send(query);

        var model = new AddCandidateViewModel
        {
            Elections = candidates.Select(c => new SelectListItem
            {
                Value = c.ElectionID.ToString(),
                Text = c.ElectionID.ToString(),
            }).ToList()
        };
        return View("~/Views/Admin/Candidate/AddCandidate.cshtml", model);
    }

    [HttpGet("EditCandidate")]
    public IActionResult EditCandidate()
    {
        return View("~/Views/Admin/Candidate/EditCandidate.cshtml");
    }

    [HttpGet("DeleteCandidate")]
    public IActionResult DeleteCandidate()
    {
        return View("~/Views/Admin/Candidate/DeleteCandidate.cshtml");
    }

    [HttpGet("ViewCandidates")]
    public async Task<IActionResult> ViewCandidates()
    {
        var query = new GetAllCandidatesQuery();
        var candidates = await _mediator.Send(query);

        var model = candidates.Select(c => new ViewCandidatesViewModel
        {
            CandidateID = c.CandidateID.ToString(),
            ElectionID = c.ElectionID.ToString(),
            FullName = c.FullName,
            Party = c.Party,
            Description = c.Description,
            Works = c.Works,
            Income = c.Income ?? 0
        }).ToList();

        return View("~/Views/Admin/Candidate/ViewCandidates.cshtml", model);
    }

    [HttpGet("validate")]
    public async Task<IActionResult> ValidateCandidate([FromQuery] int candidateId, [FromQuery] int electionId)
    {
        var query = new GetCandidateByIdQuery(candidateId);
        var candidate = await _mediator.Send(query);

        if (candidate == null)
        {
            return NotFound($"Candidate with ID {candidateId} not found.");
        }

        var isValid = candidate.ElectionID == electionId;
        return Ok(new { isValid });
    }

    [HttpPut("UpdateCandidate")]
    public async Task<IActionResult> UpdateAsync([FromBody] CandidatesPutDTO candidatesPut)
    {
        var command = new UpdateCandidateCommand(candidatesPut);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound($"Candidate with ID {candidatesPut.CandidateID} not found or Election ID does not match.");
        }

        return Ok(result);
    }

    [HttpDelete("{candidateId}")]
    public async Task<IActionResult> DeleteCandidate(int candidateId)
    {
        var command = new DeleteCandidateCommand(candidateId);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound($"Candidate with ID {candidateId} not found.");
        }

        return NoContent();
    }
}
