using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;
using OnlineVotingS.API.Validations;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Application.Services.Election.Requests.Queries;

namespace OnlineVotingS.API.Controllers.TempControllers;

[Authorize(Policy = "RequireAdminRole")]
public class ElectionController : Controller
{
    private readonly IMediator _mediator;
    private readonly IElectionValidation _electionValidation;

    public ElectionController(IMediator mediator, IElectionValidation electionValidation)
    {
        _mediator = mediator;
        _electionValidation = electionValidation;
    }

    public IActionResult GenerateElection()
    {
        return View("~/Views/Admin/Election/GenerateElection.cshtml", new GenerateElectionViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> GenerateElection(GenerateElectionViewModel model)
    {
        var validationResult = await _electionValidation.ValidateElectionAsync(model);
        if (validationResult.IsFailed)
        {
            return HandleErrorResult(validationResult, model, "~/Views/Admin/Election/GenerateElection.cshtml");
        }

        var electionDto = new ElectionsPostDTO
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate.GetValueOrDefault(),
            StartTime = model.StartTime.GetValueOrDefault(),
            EndDate = model.StartDate.GetValueOrDefault(),
            EndTime = model.EndTime.GetValueOrDefault()
        };
        var command = new CreateElectionsCommand(electionDto);
        var result = await _mediator.Send(command);

        if (result.IsFailed)
        {
            return HandleErrorResult(result, model, "~/Views/Admin/Election/GenerateElection.cshtml");
        }

        return RedirectToAction(nameof(ViewElection));
    }

    public async Task<IActionResult> GetElectionDetails(int id)
    {
        var result = await _mediator.Send(new GetElectionsByIdQuery(id));
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var election = result.Value;
        return Json(new
        {
            title = election.Title,
            description = election.Description,
            startDate = election.StartDate,
            startTime = election.StartTime,
            endDate = election.EndDate,
            endTime = election.EndTime,
            status = election.Status
        });
    }

    public async Task<IActionResult> ModifyElection()
    {
        var result = await _mediator.Send(new GetAllElectionsQuery());
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var electionList = result.Value.Select(e => new SelectListItem
        {
            Value = e.ElectionID.ToString(),
            Text = $"{e.ElectionID} - {e.Title}"
        }).ToList();

        ViewBag.Elections = new SelectList(electionList, "Value", "Text");
        return View("~/Views/Admin/Election/ModifyElection.cshtml", new ModifyElectionViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> ModifyElection(ModifyElectionViewModel model)
    {
        var validationResult = await _electionValidation.ValidateElectionAsync(model);
        if (validationResult.IsFailed)
        {
            return HandleErrorResult(validationResult, model, "~/Views/Admin/Election/ModifyElection.cshtml");
        }

        var electionDto = new ElectionsPutDTO
        {
            ElectionID = model.ElectionID,
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate.GetValueOrDefault(),
            StartTime = model.StartTime.GetValueOrDefault(),
            EndDate = model.EndDate.GetValueOrDefault(),
            EndTime = model.EndTime.GetValueOrDefault(),
            UpdatedAt = DateTime.UtcNow
        };

        var command = new UpdateElectionsCommand(electionDto);
        var result = await _mediator.Send(command);
        if (result.IsFailed)
        {
            return HandleErrorResult(result, model, "~/Views/Admin/Election/ModifyElection.cshtml");
        }
        return RedirectToAction(nameof(ViewElection));
    }

    public async Task<IActionResult> CompleteElection()
    {
        var result = await _mediator.Send(new GetCompletableElectionsQuery());
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var model = new CompleteElectionViewModel
        {
            OngoingElections = result.Value
                .Select(e => new SelectListItem { Value = e.Title, Text = e.Title })
                .ToList()
        };
        return View("~/Views/Admin/Election/CompleteElection.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> CompleteElection(CompleteElectionViewModel model)
    {
        var command = new CompleteElectionCommand(model.SelectedTitle);
        var result = await _mediator.Send(command);
        if (result.IsFailed)
        {
            return HandleErrorResult(result, model, "~/Views/Admin/Election/CompleteElection.cshtml");
        }
        return RedirectToAction(nameof(ViewElection));
    }

    public async Task<IActionResult> DeleteElection()
    {
        var result = await _mediator.Send(new GetAllElectionsQuery());
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var model = new DeleteElectionViewModel
        {
            AvailableElections = result.Value.Select(e => new SelectListItem
            {
                Value = e.ElectionID.ToString(),
                Text = $"{e.ElectionID} - {e.Title}"
            }).ToList()
        };
        return View("~/Views/Admin/Election/DeleteElection.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteElection(DeleteElectionViewModel model)
    {
        var command = new DeleteElectionsCommand(model.SelectedElectionID);
        var result = await _mediator.Send(command);
        if (result.IsFailed)
        {
            return HandleErrorResult(result, model, "~/Views/Admin/Election/DeleteElection.cshtml");
        }
        return RedirectToAction(nameof(ViewElection));
    }

    public async Task<IActionResult> ViewElection()
    {
        var result = await _mediator.Send(new GetAllElectionsQuery());
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var model = result.Value.Select(e => new ViewElectionViewModel
        {
            ElectionID = e.ElectionID,
            Title = e.Title,
            Description = e.Description,
            StartDate = e.StartDate,
            StartTime = e.StartTime,
            EndDate = e.EndDate,
            EndTime = e.EndTime,
            Status = e.Status
        }).ToList();
        return View("~/Views/Admin/Election/ViewElection.cshtml", model);
    }

    private IActionResult HandleErrorResult(Result result, object? model = null, string? viewPath = null)
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