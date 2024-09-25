using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.VoterViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using MediatR;
using OnlineVotingS.API.Models.AdminViewModels.ComplaintViewModels;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using Microsoft.AspNetCore.Identity;
using OnlineVotingS.Domain.Models;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Application.Services.Vote.Requests.Queries;
using OnlineVotingS.Domain.Enums;
using OnlineVotingS.Application.Services.Vote.Requests.Commands;
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
        var candidates = await _mediator.Send(new GetAllCandidatesQuery());
        var viewModel = candidates.Select(c => new CandidateViewModel
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
    
            var elections = await _mediator.Send(new GetAllElectionsQuery());
            var userId = _userManager.GetUserId(User);
            var viewModel = new List<ElectionsViewModel>();

            foreach (var election in elections)
            {
                var hasVoted = await _mediator.Send(new GetVotesByUserIDQuery(userId, election.ElectionID));

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
        var election = await _mediator.Send(new GetElectionsByIdQuery(electionId));

        if (election.Status != ElectionStatus.Active)
        {
            return RedirectToAction("ElectionPage");
        }

        var candidates = await _mediator.Send(new GetCandidatesByElectionIdQuery(electionId));
        var viewModel = candidates.Select(c => new CandidateViewModel
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
            await _mediator.Send(command);
            TempData["VoteMessage"] = "Your vote has been cast successfully!";
            TempData["VoteMessageType"] = "success";
        }
        catch (InvalidVoteException ex)
        {
            TempData["VoteMessage"] = ex.Message;
            TempData["VoteMessageType"] = "error";
        }
        catch (Exception)
        {
            TempData["VoteMessage"] = "An error occurred while casting your vote. Please try again.";
            TempData["VoteMessageType"] = "error";
        }

        return RedirectToAction("ElectionPage");
    }

    [HttpGet]
    public async Task<IActionResult> ComplainPage()
    {
        try
        {
            var allElections = await _mediator.Send(new GetAllElectionsQuery());
            var elections = allElections.Select(e => new SelectListItem
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
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Unable to load elections. Please try again later.";
            return View(new ComplainViewModel());
        }
    }

    [HttpPost]
    public async Task<IActionResult> SubmitComplain(ComplainViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var allElections = await _mediator.Send(new GetAllElectionsQuery());
            model.Elections = allElections.Select(e => new SelectListItem
            {
                Value = e.ElectionID.ToString(),
                Text = e.Title
            });
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

        try
        {
            var complaint = new ComplaintsPostDTO
            {
                ElectionID = model.ElectionID,
                ComplaintText = model.ComplaintText,
                UserID = userId
            };

            var command = new CreateComplaintCommand(complaint);
            await _mediator.Send(command);

            TempData["SuccessMessage"] = "Your complaint has been successfully submitted.";
            return RedirectToAction("ComplainPage");
        }
        catch (Exception ex)
        { 

            ModelState.AddModelError("", "An error occurred while submitting your complaint. Please try again.");
            var allElections = await _mediator.Send(new GetAllElectionsQuery());
            model.Elections = allElections.Select(e => new SelectListItem
            {
                Value = e.ElectionID.ToString(),
                Text = e.Title
            });

            return View("ComplainPage", model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RepliedComplaintsPage()
    {
        if (User.Identity!.IsAuthenticated)
        {
            var userId = _userManager.GetUserId(User);

            // Get all complaints for the current user
            var userComplaints = await _mediator.Send(new GetComplaintsByUserIdCommand(userId));

            var viewModel = new List<RepliedComplaintsViewModel>();

            foreach (var complaint in userComplaints)
            {
                var replies = await _mediator.Send(new GetRepliedComplaintsByComplaintIDQuery(complaint.ComplaintID));

                viewModel.AddRange(replies.Select(r => new RepliedComplaintsViewModel
                {
                    RepliedComplaintID = r.RepliedComplaintID,
                    ComplaintID = r.ComplaintID,
                    ComplaintText = complaint.ComplaintText,
                    ComplaintDate = complaint.ComplaintDate,
                    ReplyText = r.ReplyText,
                    ReplyDate = r.ReplyDate,
                }));
            }

            return View("~/Views/Voter/RepliedComplaintsPage.cshtml", viewModel);
        }

        return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
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