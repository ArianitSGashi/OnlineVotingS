using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.GuestViewModels;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Models;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class GuestController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public GuestController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public IActionResult GuestDashboard()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CandidatePage()
    {
        var result = await _mediator.Send(new GetAllCandidatesQuery());
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var viewModel = result.Value.Select(c => new CandidateViewModel
        {
            CandidateID = c.CandidateID,
            ElectionID = c.ElectionID,
            FullName = c.FullName,
            Party = c.Party,
            Description = c.Description,
            Income = c.Income,
            Works = c.Works
        }).ToList();

        return View("~/Views/Guest/CandidatePage.cshtml", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ElectionPage()
    {
        var electionsResult = await _mediator.Send(new GetAllElectionsQuery());
        if (electionsResult.IsFailed)
        {
            return HandleErrorResult(electionsResult);
        }

        var userId = _userManager.GetUserId(User);
        var viewModel = new List<ElectionsViewModel>();

        foreach (var election in electionsResult.Value)
        {
            var hasVotedResult = await _mediator.Send(new GetVotesByUserIDQuery(userId, election.ElectionID));
            var hasVoted = hasVotedResult.IsSuccess && hasVotedResult.Value;

            viewModel.Add(new ElectionsViewModel
            {
                ElectionID = election.ElectionID,
                Title = election.Title ?? string.Empty,
                Description = election.Description ?? string.Empty,
                StartDate = election.StartDate,
                StartTime = election.StartTime,
                EndDate = election.EndDate,
                EndTime = election.EndTime,
                Status = election.Status,
                UserHasVoted = hasVoted
            });
        }

        ViewBag.VoteMessage = TempData["VoteMessage"];
        ViewBag.VoteMessageType = TempData["VoteMessageType"];

        return View(viewModel);
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