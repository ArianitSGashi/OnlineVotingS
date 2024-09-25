using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using MediatR;
using OnlineVotingS.API.Models.VoterViewModels;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Models;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Domain.CostumExceptions;
using OnlineVotingS.API.Models;
using System.Diagnostics;
using OnlineVotingS.Application.DTO.GetDTO;
using OnlineVotingS.Application.Services.Vote.Handlers.Queries;

namespace OnlineVotingS.API.Controllers.TempControllers;

[Authorize(Policy = "RequireVoterRole")]
public class VoterController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public VoterController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [Authorize(Policy = "RequireVoterRole")]
    public IActionResult VoterDashboard()
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

        return View("~/Views/Voter/CandidatePage.cshtml", viewModel);
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

    [HttpGet]
    public async Task<IActionResult> VotePage(int electionId)
    {
        var electionResult = await _mediator.Send(new GetElectionsByIdQuery(electionId));
        if (electionResult.IsFailed)
        {
            return HandleErrorResult(electionResult);
        }

        if (electionResult.Value.Status != ElectionStatus.Active)
        {
            return RedirectToAction("ElectionPage");
        }

        var candidatesResult = await _mediator.Send(new GetCandidatesByElectionIdQuery(electionId));
        if (candidatesResult.IsFailed)
        {
            return HandleErrorResult(candidatesResult);
        }

        var viewModel = candidatesResult.Value.Select(c => new CandidateViewModel
        {
            CandidateID = c.CandidateID,
            ElectionID = c.ElectionID,
            FullName = c.FullName,
            Party = c.Party,
            Description = c.Description,
            Income = c.Income,
            Works = c.Works
        }).ToList();

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CastVote(int candidateId, int electionId)
    {
        var userId = _userManager.GetUserId(User);

        var voteDto = new VotesPostDTO
        {
            UserID = userId,
            ElectionID = electionId,
            CandidateID = candidateId,
            VoteDate = DateTime.UtcNow
        };

        var command = new CreateVoteCommand(voteDto);

        try
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                TempData["VoteMessage"] = "Your vote has been cast successfully!";
                TempData["VoteMessageType"] = "success";
            }
            else
            {
                return HandleErrorResult(result);
            }
        }
        catch (InvalidVoteException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View("Error");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "An error occurred while casting your vote. Please try again.");
            return View("Error");
        }

        return RedirectToAction("ElectionPage");
    }

    [HttpGet]
    public async Task<IActionResult> ComplainPage()
    {
        var allElectionsResult = await _mediator.Send(new GetAllElectionsQuery());
        if (allElectionsResult.IsFailed)
        {
            return HandleErrorResult(allElectionsResult);
        }

        var elections = allElectionsResult.Value.Select(e => new SelectListItem
        {
            Value = e.ElectionID.ToString(),
            Text = e.Title
        });

        var complainViewModel = new ComplainViewModel
        {
            ElectionID = 0,
            ComplaintText = string.Empty,
            Elections = elections,
        };

        return View(complainViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitComplain(ComplainViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var allElectionsResult = await _mediator.Send(new GetAllElectionsQuery());
            if (allElectionsResult.IsSuccess)
            {
                model.Elections = allElectionsResult.Value.Select(e => new SelectListItem
                {
                    Value = e.ElectionID.ToString(),
                    Text = e.Title
                });
            }
            return View("ComplainPage", model);
        }

        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            ModelState.AddModelError("", "Unable to process your request. Please try again later.");
            return View("ComplainPage", model);
        }

        var complaint = new ComplaintsPostDTO
        {
            ElectionID = model.ElectionID,
            ComplaintText = model.ComplaintText,
            UserID = userId
        };

        var command = new CreateComplaintCommand(complaint);
        var result = await _mediator.Send(command);

        if (result.IsFailed)
        {
            return HandleErrorResult(result, model, "ComplainPage");
        }

        TempData["SuccessMessage"] = "Your complaint has been successfully submitted.";
        return RedirectToAction("ComplainPage");
    }

    [HttpGet]
    public async Task<IActionResult> RepliedComplaintsPage()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = _userManager.GetUserId(User);

        var userComplaintsResult = await _mediator.Send(new GetComplaintsByUserIdCommand(userId));
        if (userComplaintsResult.IsFailed)
        {
            return HandleErrorResult(userComplaintsResult);
        }

        var viewModel = new List<RepliedComplaintsViewModel>();

        foreach (var complaint in userComplaintsResult.Value)
        {
            var repliesResult = await _mediator.Send(new GetRepliedComplaintsByComplaintIDQuery(complaint.ComplaintID));
            if (repliesResult.IsSuccess)
            {
                viewModel.AddRange(repliesResult.Value.Select(r => new RepliedComplaintsViewModel
                {
                    RepliedComplaintID = r.RepliedComplaintID,
                    ComplaintID = r.ComplaintID,
                    ComplaintText = complaint.ComplaintText,
                    ComplaintDate = complaint.ComplaintDate,
                    ReplyText = r.ReplyText,
                    ReplyDate = r.ReplyDate,
                }));
            }
        }

        return View("~/Views/Voter/RepliedComplaintsPage.cshtml", viewModel);
    }

    private IActionResult HandleErrorResult<T>(Result<T> result, object? model = null, string? viewName = null)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Message);
        }

        if (model != null && !string.IsNullOrEmpty(viewName))
        {
            return View(viewName, model);
        }

        return View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> ShowResults()
    {
        var allElections = await _mediator.Send(new GetAllElectionsQuery());
        
        return View("~/Views/Voter/ShowResults.cshtml", allElections);
        
    }
    
    [HttpGet]
    public async Task<IActionResult> GetResultsForElection(int electionId)
    {
        var electionResults = await _mediator.Send(new FilterVotesByCandidateQuery(electionId));

        var candidateVotesEnumerable = electionResults.ToList();
        var resultData = new
        {
            labels = candidateVotesEnumerable.Select(r => r.CandidateFullName).ToArray(),
            votes = candidateVotesEnumerable.Select(r => r.Votes).ToArray()
        };

        return Json(resultData);
    }

}