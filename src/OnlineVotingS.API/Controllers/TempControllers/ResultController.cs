using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.ResultViewModels;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Application.Services.Results.Requests.Queries;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class ResultController : Controller
{
    private readonly IMediator _mediator;

    public ResultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GenerateResult()
    {
        var allElectionsResult = await _mediator.Send(new GetAllElectionsQuery());
        if (allElectionsResult.IsFailed)
        {
            return HandleErrorResult(allElectionsResult);
        }

        var model = new GenerateResultViewModel
        {
            OngoingElections = allElectionsResult.Value.ToList(),
        };
        return View("~/Views/Admin/Result/GenerateResult.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> GenerateResult(int SelectedElectionID, int? CandidateId)
    {
        var result = await _mediator.Send(new GenerateOrUpdateResultsCommand(SelectedElectionID, CandidateId));
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }
        return RedirectToAction("ViewResult");
    }

    [HttpGet]
    public async Task<IActionResult> GetCandidatesByElection(int electionId)
    {
        var candidatesResult = await _mediator.Send(new GetCandidatesByElectionIdQuery(electionId));
        if (candidatesResult.IsFailed)
        {
            return Json(new { error = "Failed to retrieve candidates", details = candidatesResult.Errors.Select(e => e.Message) });
        }
        return Json(candidatesResult.Value);
    }

    [HttpGet]
    public async Task<IActionResult> ViewResult()
    {
        var model = new List<ViewResultViewModel>();
        var allResultsResult = await _mediator.Send(new GetAllResultsQuery());
        if (allResultsResult.IsFailed)
        {
            return HandleErrorResult(allResultsResult);
        }

        foreach (var item in allResultsResult.Value)
        {
            var candidateResult = await _mediator.Send(new GetCandidateByIdQuery(item.CandidateID));
            var electionResult = await _mediator.Send(new GetElectionsByIdQuery(item.ElectionID));

            if (candidateResult.IsFailed || electionResult.IsFailed)
            {
                continue; 
            }

            model.Add(new ViewResultViewModel
            {
                ResultID = item.ResultID,
                CandidateID = candidateResult.Value.CandidateID,
                CandidateName = candidateResult.Value.FullName,
                ElectionID = electionResult.Value.ElectionID,
                ElectionTitle = electionResult.Value.Title,
                TotalVotes = item.TotalVotes
            });
        }

        return View("~/Views/Admin/Result/ViewResult.cshtml", model);
    }

    private IActionResult HandleErrorResult<T>(Result<T> result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Message);
        }

        return View("Error");
    }
}