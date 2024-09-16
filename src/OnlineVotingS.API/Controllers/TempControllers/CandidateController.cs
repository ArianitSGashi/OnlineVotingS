using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.TempControllers;

public class CandidateController : Controller
{
    private readonly IMediator _mediator;

    public CandidateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> AddCandidate()
    {
        var elections = await _mediator.Send(new GetAllElectionsQuery());
        var viewModel = new AddCandidateViewModel
        {
            Elections = elections.Select(e => new SelectListItem
            {
                Value = e.ElectionID.ToString(),
                Text = e.Title
            }).ToList()
        };
        return View("~/Views/Admin/Candidate/AddCandidate.cshtml", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddCandidate(AddCandidateViewModel model)
    {
       
        var candidateDto = new CandidatesPostDTO
        {
            ElectionID = model.ElectionID,
            FullName = model.FullName,
            Party = model.Party,
            Description = model.Description,
            Income = model.Income,
            Works = model.Works
        };

        var command = new CreateCandidateCommand(candidateDto);
        var result = await _mediator.Send(command);
        return RedirectToAction(nameof(ViewCandidates));
    }

    [HttpGet]
    public async Task<IActionResult> EditCandidate()
    {
        var candidates = await _mediator.Send(new GetAllCandidatesQuery());
        var elections = await _mediator.Send(new GetAllElectionsQuery());
        var model = new EditCandidateViewModel
        {
            CandidateList = candidates.Select(c => new SelectListItem
            {
                Value = c.CandidateID.ToString(),
                Text = $"{c.CandidateID} - {c.FullName}"
            }).ToList()
        };
        return View("~/Views/Admin/Candidate/EditCandidate.cshtml", model);
    }

    [HttpGet]
    public async Task<IActionResult> GetCandidateDetails(int id)
    {
        var candidate = await _mediator.Send(new GetCandidateByIdQuery(id));
        if (candidate == null)
        {
            return NotFound();
        }
        var candidateDetails = new
        {
            electionId = candidate.ElectionID,
            fullName = candidate.FullName,
            party = candidate.Party,
            description = candidate.Description,
            income = candidate.Income,
            works = candidate.Works
        };
        return Json(candidateDetails);
    }

    [HttpPost]
    public async Task<IActionResult> EditCandidate(EditCandidateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var command = new UpdateCandidateCommand(new CandidatesPutDTO
            {
                CandidateID = model.CandidateID,
                ElectionID = model.ElectionID,
                FullName = model.FullName,
                Party = model.Party,
                Description = model.Description,
                Income = model.Income,
                Works = model.Works
            });

            var result = await _mediator.Send(command);
            if (result != null)
            {
                return RedirectToAction(nameof(ViewCandidates));
            }
            ModelState.AddModelError(string.Empty, "Failed to update candidate.");
        }

        // If we got this far, something failed; redisplay form
        var candidates = await _mediator.Send(new GetAllCandidatesQuery());
        var elections = await _mediator.Send(new GetAllElectionsQuery());
        model.CandidateList = candidates.Select(c => new SelectListItem
        {
            Value = c.CandidateID.ToString(),
            Text = $"{c.CandidateID} - {c.FullName}"
        }).ToList();
        return View("~/Views/Admin/Candidate/EditCandidate.cshtml", model);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCandidate()
    {
        var candidates = await _mediator.Send(new GetAllCandidatesQuery());
        var viewModel = new DeleteCandidateViewModel
        {
            AvailableCandidates = candidates.Select(c => new SelectListItem
            {
                Value = c.CandidateID.ToString(),
                Text = $"{c.CandidateID} - {c.FullName}"
            }).ToList()
        };
        return View("~/Views/Admin/Candidate/DeleteCandidate.cshtml", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCandidate(DeleteCandidateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var command = new DeleteCandidateCommand(model.CandidateID);
            await _mediator.Send(command);
            return RedirectToAction(nameof(ViewCandidates));
        }

        // If we got this far, something failed; redisplay form
        var candidates = await _mediator.Send(new GetAllCandidatesQuery());
        model.AvailableCandidates = candidates.Select(c => new SelectListItem
        {
            Value = c.CandidateID.ToString(),
            Text = $"{c.CandidateID} - {c.FullName}"
        }).ToList();
        return View("~/Views/Admin/Candidate/DeleteCandidate.cshtml", model);
    }

    [HttpGet]
    public async Task<IActionResult> ViewCandidates()
    {
        var candidates = await _mediator.Send(new GetAllCandidatesQuery());
        var viewModel = candidates.Select(c => new ViewCandidatesViewModel
        {
            CandidateID = c.CandidateID,
            ElectionID = c.ElectionID,
            FullName = c.FullName,
            Party = c.Party,
            Description = c.Description,
            Income = c.Income,
            Works = c.Works
        }).ToList();

        return View("~/Views/Admin/Candidate/ViewCandidates.cshtml", viewModel);
    }
}