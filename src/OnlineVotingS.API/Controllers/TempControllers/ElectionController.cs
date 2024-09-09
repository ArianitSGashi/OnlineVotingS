using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Controllers;

public class ElectionController : Controller
{
    private readonly IMediator _mediator;

    public ElectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult GenerateElection()
    {
        return View("~/Views/Admin/Election/GenerateElection.cshtml", new GenerateElectionViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> GenerateElection(GenerateElectionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("~/Views/Admin/Election/GenerateElection.cshtml", model);
        }
        var electionDto = new ElectionsPostDTO
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            StartTime = model.StartTime,
            EndDate = model.EndDate,
            EndTime = model.EndTime
        };
        var command = new CreateElectionsCommand(electionDto);
        var result = await _mediator.Send(command);
        if (result != null)
        {
            TempData["SuccessMessage"] = $"Election '{model.Title}' has been successfully created.";
            return RedirectToAction(nameof(ViewElection));
        }

        ModelState.AddModelError("", "An error occurred while creating the election. Please try again.");
        return View("~/Views/Admin/Election/GenerateElection.cshtml", model);
    }

    public async Task<IActionResult> GetElectionDetails(int id)
    {
        var election = await _mediator.Send(new GetElectionsByIdQuery(id));
        if (election == null)
        {
            return NotFound();
        }

        return Json(new
        {
            title = election.Title,
            description = election.Description,
            startDate = election.StartDate,
            startTime = election.StartTime,
            endDate = election.EndDate,
            endTime = election.EndTime
        });
    }

    public async Task<IActionResult> ModifyElection()
    {
        var elections = await _mediator.Send(new GetAllElectionsQuery());
        var electionList = elections.Select(e => new SelectListItem
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
        if (!ModelState.IsValid)
        {
            var elections = await _mediator.Send(new GetAllElectionsQuery());
            ViewBag.Elections = elections;
            return View("~/Views/Admin/Election/ModifyElection.cshtml", model);
        }

        var electionDto = new ElectionsPutDTO
        {
            ElectionID = model.ElectionID,
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            StartTime = model.StartTime,
            EndDate = model.EndDate,
            EndTime = model.EndTime,
            UpdateAt = model.UpdatedAt
        };

        var command = new UpdateElectionsCommand(electionDto);
        var result = await _mediator.Send(command);

        if (result != null)
        {
            TempData["SuccessMessage"] = $"Election '{model.Title}' has been successfully updated.";
            return RedirectToAction(nameof(ViewElection));
        }

        ModelState.AddModelError("", "An error occurred while updating the election. Please try again.");
        var electionsForDropdown = await _mediator.Send(new GetAllElectionsQuery());
        ViewBag.Elections = electionsForDropdown;
        return View("~/Views/Admin/Election/ModifyElection.cshtml", model);
    }

    public async Task<IActionResult> CompleteElection()
    {
        var activeElections = await _mediator.Send(new GetActiveElectionsQuery());
        var model = new CompleteElectionViewModel
        {
            OngoingElections = activeElections
                .Select(e => new SelectListItem { Value = e.Title, Text = e.Title })
                .ToList()
        };
        return View("~/Views/Admin/Election/CompleteElection.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> CompleteElection(CompleteElectionViewModel model)
    {
        if (string.IsNullOrEmpty(model.SelectedTitle))
        {
            ModelState.AddModelError("SelectedTitle", "Please select an election to complete.");
            return View("~/Views/Admin/Election/CompleteElection.cshtml", model);
        }

        var command = new CompleteElectionCommand(model.SelectedTitle);
        var result = await _mediator.Send(command);

        if (result)
        {
            TempData["SuccessMessage"] = $"Election '{model.SelectedTitle}' has been successfully completed.";
        }
        else
        {
            TempData["WarningMessage"] = $"Election '{model.SelectedTitle}' could not be completed. It may already be completed or not found.";
        }

        return RedirectToAction(nameof(ViewElection));
    }

    public IActionResult DeleteElection()
    {
        return View("~/Views/Admin/Election/DeleteElection.cshtml", new DeleteElectionViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> DeleteElection(DeleteElectionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("~/Views/Admin/Election/DeleteElection.cshtml", model);
        }

        var command = new DeleteElectionsCommand(model.ElectionID);
        var result = await _mediator.Send(command);

        if (result)
        {
            TempData["SuccessMessage"] = "Election has been successfully deleted.";
        }
        else
        {
            TempData["WarningMessage"] = "Election could not be deleted. It may not exist or there was an error.";
        }

        return RedirectToAction(nameof(ViewElection));
    }

    public async Task<IActionResult> ViewElection()
    {
        var elections = await _mediator.Send(new GetAllElectionsQuery());
        var model = elections.Select(e => new ViewElectionViewModel
        {
            ElectionID = e.ElectionID,
            Title = e.Title,
            Description = e.Description,
            StartDate = e.StartDate,
            StartTime = e.StartTime,
            EndDate = e.EndDate,
            EndTime = e.EndTime
        }).ToList();
        return View("~/Views/Admin/Election/ViewElection.cshtml", model);
    }
}