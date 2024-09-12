using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Validations;

namespace OnlineVotingS.API.Controllers;

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
        var (isValid, errors) = await _electionValidation.ValidateElectionAsync(model);
        if (!isValid)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
            return View("~/Views/Admin/Election/GenerateElection.cshtml", model);
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
        return RedirectToAction(nameof(ViewElection));
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
            endTime = election.EndTime,
            status = election.Status
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
        var (isValid, errors) = await _electionValidation.ValidateElectionAsync(model);
        if (!isValid)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
            var elections = await _mediator.Send(new GetAllElectionsQuery());
            ViewBag.Elections = new SelectList(elections, "ElectionID", "Title");
            return View("~/Views/Admin/Election/ModifyElection.cshtml", model);
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
        return RedirectToAction(nameof(ViewElection));
    }

    public async Task<IActionResult> CompleteElection()
    {
        var completableElections = await _mediator.Send(new GetCompletableElectionsQuery());
        var model = new CompleteElectionViewModel
        {
            OngoingElections = completableElections
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
        return RedirectToAction(nameof(ViewElection));
    }

    public async Task<IActionResult> DeleteElection()
    {
        var elections = await _mediator.Send(new GetAllElectionsQuery());
        var model = new DeleteElectionViewModel
        {
            AvailableElections = elections.Select(e => new SelectListItem
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
            EndTime = e.EndTime,
            Status = e.Status
        }).ToList();
        return View("~/Views/Admin/Election/ViewElection.cshtml", model);
    }
}