using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.ResultViewModels;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.Services.Results.Requests.Commands;
using OnlineVotingS.Application.Services.Results.Requests.Queries;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

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
        bool resultExists = false;
        var results = await _mediator.Send(new GetAllResultsQuery());
        var votesForElection = await _mediator.Send(new GetVotesByElectionIDQuery(SelectedElectionID));
        
        if (CandidateId != null)
        {
            resultExists = results.Any(x=> x.CandidateID == CandidateId && x.ElectionID == SelectedElectionID);
            votesForElection = votesForElection.Where(x => x.CandidateID == CandidateId);
        }
        else
        {
            resultExists = results.Any(x=> x.ElectionID == SelectedElectionID);
        }
        if (!resultExists)
        {
            var votesBasedOnCandidates = votesForElection.GroupBy(x => x.CandidateID);
            foreach (var vote in votesBasedOnCandidates.ToList())
            {
                ResultPostDTO result = new ResultPostDTO()
                {
                    TotalVotes = vote.Count(),
                    CandidateID = vote.Key,
                    ElectionID = SelectedElectionID
                };
                await _mediator.Send(new CreateResultCommand(result));
            }
        }
        else
        {
            TempData["Message"] = "Results for this election have already been generated.";
            return RedirectToAction("GenerateResult");
        }

        return await ViewResult();
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
