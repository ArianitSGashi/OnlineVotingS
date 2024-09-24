using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Controllers.TempControllers;

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
        var electionsResult = await _mediator.Send(new GetAllElectionsQuery());

        if (electionsResult.IsFailed)
        {
            return View("Error", electionsResult.Errors);
        }

        var viewModel = new AddCandidateViewModel
        {
            Elections = electionsResult.Value.Select(e => new SelectListItem
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

        if (result.IsFailed)
        {
            ModelState.AddModelError(string.Empty, "Failed to create candidate.");
            return View("~/Views/Admin/Candidate/AddCandidate.cshtml", model);
        }

        return RedirectToAction(nameof(ViewCandidates));
    }

    [HttpGet]
    public async Task<IActionResult> EditCandidate()
    {
        var candidatesResult = await _mediator.Send(new GetAllCandidatesQuery());
        var electionsResult = await _mediator.Send(new GetAllElectionsQuery());

        if (candidatesResult.IsFailed || electionsResult.IsFailed)
        {
            return View("Error", candidatesResult.Errors.Concat(electionsResult.Errors));
        }

        var model = new EditCandidateViewModel
        {
            CandidateList = candidatesResult.Value.Select(c => new SelectListItem
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
        var candidateResult = await _mediator.Send(new GetCandidateByIdQuery(id));

        if (candidateResult.IsFailed)
        {
            return NotFound();
        }

        var candidate = candidateResult.Value;
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
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(ViewCandidates));
            }
            ModelState.AddModelError(string.Empty, "Failed to update candidate.");
        }

        // If we got this far, something failed; redisplay form
        var candidatesResult = await _mediator.Send(new GetAllCandidatesQuery());
        var electionsResult = await _mediator.Send(new GetAllElectionsQuery());

        if (candidatesResult.IsFailed || electionsResult.IsFailed)
        {
            return View("Error", candidatesResult.Errors.Concat(electionsResult.Errors));
        }

        model.CandidateList = candidatesResult.Value.Select(c => new SelectListItem
        {
            Value = c.CandidateID.ToString(),
            Text = $"{c.CandidateID} - {c.FullName}"
        }).ToList();
        return View("~/Views/Admin/Candidate/EditCandidate.cshtml", model);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCandidate()
    {
        var candidatesResult = await _mediator.Send(new GetAllCandidatesQuery());

        if (candidatesResult.IsFailed)
        {
            return View("Error", candidatesResult.Errors);
        }

        var viewModel = new DeleteCandidateViewModel
        {
            AvailableCandidates = candidatesResult.Value.Select(c => new SelectListItem
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
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(ViewCandidates));
            }
            ModelState.AddModelError(string.Empty, "Failed to delete candidate.");
        }

        // If we got this far, something failed; redisplay form
        var candidatesResult = await _mediator.Send(new GetAllCandidatesQuery());

        if (candidatesResult.IsFailed)
        {
            return View("Error", candidatesResult.Errors);
        }

        model.AvailableCandidates = candidatesResult.Value.Select(c => new SelectListItem
        {
            Value = c.CandidateID.ToString(),
            Text = $"{c.CandidateID} - {c.FullName}"
        }).ToList();
        return View("~/Views/Admin/Candidate/DeleteCandidate.cshtml", model);
    }

    [HttpGet]
    public async Task<IActionResult> ViewCandidates()
    {
        var candidatesResult = await _mediator.Send(new GetAllCandidatesQuery());

        if (candidatesResult.IsFailed)
        {
            return View("Error", candidatesResult.Errors);
        }

        var viewModel = candidatesResult.Value.Select(c => new ViewCandidatesViewModel
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