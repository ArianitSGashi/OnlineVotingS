using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Domain.Entities;

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
            return HandleErrorResult(electionsResult);
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
            return HandleErrorResult(result, model, "~/Views/Admin/Candidate/AddCandidate.cshtml");
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
            return HandleErrorResult(candidatesResult, electionsResult);
        }

        var model = new EditCandidateViewModel
        {
            CandidateList = candidatesResult.Value.Select(c => new SelectListItem
            {
                Value = c.CandidateID.ToString(),
                Text = $"{c.CandidateID} - {c.FullName}"
            }).ToList(),

            Elections = electionsResult.Value.Select(e => new SelectListItem
            {
                Value = e.ElectionID.ToString(),
                Text = e.Title
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
            return HandleErrorResult(candidateResult);
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
            return HandleErrorResult(result, model, "~/Views/Admin/Candidate/EditCandidate.cshtml");
        }

        var candidatesResult = await _mediator.Send(new GetAllCandidatesQuery());
        var electionsResult = await _mediator.Send(new GetAllElectionsQuery());

        if (candidatesResult.IsFailed || electionsResult.IsFailed)
        {
            return HandleErrorResult(candidatesResult, electionsResult);
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
            return HandleErrorResult(candidatesResult);
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
            return HandleErrorResult(result, model, "~/Views/Admin/Candidate/DeleteCandidate.cshtml");
        }

        var candidatesResult = await _mediator.Send(new GetAllCandidatesQuery());

        if (candidatesResult.IsFailed)
        {
            return HandleErrorResult(candidatesResult);
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
            return HandleErrorResult(candidatesResult);
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

    private IActionResult HandleErrorResult<T>(Result<T> result, object? model = null, string? viewPath = null)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Message);
        }

        if (model != null && viewPath != null)
        {
            return View(viewPath, model);
        }
        return View("Error");
    }
}