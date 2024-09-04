using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Infrastructure.Persistence.Context; // Ensure this path is correct

namespace OnlineVotingS.API.Controllers.TempControllers;

public class CandidateController : Controller
{
    private readonly ApplicationDbContext _context;

    public CandidateController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult AddCandidate()
    {
        var model = new AddCandidateViewModel
        {
            Elections = _context.Candidates.Select(c => new SelectListItem
            {
                Value = c.ElectionID.ToString(),
                Text = c.ElectionID.ToString(),
            }).ToList()
        };
        return View("~/Views/Admin/Candidate/AddCandidate.cshtml", model);
    }

    [HttpGet]
    public IActionResult EditCandidate()
    {
        return View("~/Views/Admin/Candidate/EditCandidate.cshtml");
    }

    [HttpGet]
    public IActionResult DeleteCandidate()
    {
        return View("~/Views/Admin/Candidate/DeleteCandidate.cshtml");
    }

    [HttpGet]
    public async Task<IActionResult> ViewCandidates()
    {
        var candidates = await _context.Candidates
            .Select(c => new ViewCandidatesViewModel
            {
                CandidateID = c.CandidateID.ToString(),
                ElectionID = c.ElectionID.ToString(),
                FullName = c.FullName,
                Party = c.Party,
                Description = c.Description,
                Works = c.Works,
                Income = c.Income ?? 0, // Default to 0 if Income is null
            }).ToListAsync();

        return View("~/Views/Admin/Candidate/ViewCandidates.cshtml", candidates);
    }

    [HttpGet("validate")]
    public async Task<IActionResult> ValidateCandidate([FromQuery] int candidateId, [FromQuery] int electionId)
    {
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(c => c.CandidateID == candidateId);

        if (candidate == null)
        {
            return NotFound($"Candidate with ID {candidateId} not found.");
        }

        var isValid = candidate.ElectionID == electionId;
        return Ok(new { isValid });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CandidatesPutDTO candidatesPut)
    {
        var existingCandidate = await _context.Candidates
            .FirstOrDefaultAsync(c => c.CandidateID == candidatesPut.CandidateID);

        if (existingCandidate == null)
        {
            return NotFound($"Candidate with ID {candidatesPut.CandidateID} not found.");
        }

        if (existingCandidate.ElectionID != candidatesPut.ElectionID)
        {
            return BadRequest($"Election ID {candidatesPut.ElectionID} does not match with Candidate ID {candidatesPut.CandidateID}.");
        }

        existingCandidate.FullName = candidatesPut.FullName;
        existingCandidate.Party = candidatesPut.Party;
        existingCandidate.Description = candidatesPut.Description;
        existingCandidate.Income = candidatesPut.Income;
        existingCandidate.Works = candidatesPut.Works;

        await _context.SaveChangesAsync();

        return Ok(existingCandidate);
    }
}
