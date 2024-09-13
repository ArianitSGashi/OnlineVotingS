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
        var allElections = await _mediator.Send(new GetAllElectionsQuery());
        var model = new GenerateResultViewModel
        {
            OngoingElections = allElections.ToList(),
        };
        return View("~/Views/Admin/Result/GenerateResult.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> GenerateResult(int SelectedElectionID, int? CandidateId)
    {
        await _mediator.Send(new GenerateOrUpdateResultsCommand(SelectedElectionID, CandidateId));
        return RedirectToAction("ViewResult");
    }

    [HttpGet]
    public async Task<IActionResult> GetCandidatesByElection(int electionId)
    {
        var candidates = await _mediator.Send(new GetCandidatesByElectionIdQuery(electionId));

        return Json(candidates);
    }

    [HttpGet]
    public async Task<IActionResult> ViewResult()
    {
        var model = new List<ViewResultViewModel>();
        var allResults = await _mediator.Send(new GetAllResultsQuery());

        foreach (var item in allResults)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery(item.CandidateID));
            var election = await _mediator.Send(new GetElectionsByIdQuery(item.ElectionID));
            model.Add(new ViewResultViewModel
            {
                ResultID = item.ResultID,
                CandidateID = candidate.CandidateID,
                CandidateName = candidate.FullName,
                ElectionID = election.ElectionID,
                ElectionTitle = election.Title,
                TotalVotes = item.TotalVotes
            });
        }
        
        return View("~/Views/Admin/Result/ViewResult.cshtml", model);
    }
}