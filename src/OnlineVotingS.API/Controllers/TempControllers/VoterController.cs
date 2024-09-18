using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineVotingS.API.Models.VoterViewModels;
using System.Linq;
using System.Threading.Tasks;
using OnlineVotingS.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.Application.Services.Election.Requests.Queries;
using MediatR;
using OnlineVotingS.API.Models.AdminViewModels.ComplaintViewModels;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using Microsoft.AspNetCore.Identity;
using OnlineVotingS.Domain.Models;

namespace OnlineVotingS.API.Controllers.TempControllers
{
    [Authorize(Policy = "RequireVoterRole")]
    public class VoterController : Controller
    {
        private readonly ApplicationDbContext _context;  // Inject the DbContext
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public VoterController(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _context = context;  // Set the DbContext in the constructor
            _mediator = mediator;
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireVoterRole")]
        public IActionResult VoterDashboard()
        {
            return View();
        }

        // GET: /Voter/Candidates
        public async Task<IActionResult> CandidatePage()
        {
            // Fetch candidates from the database using EF Core
            var candidates = await _context.Candidates
                .Select(c => new CandidateViewModel
                {
                    CandidateID = c.CandidateID,
                    ElectionID = c.ElectionID,
                    FullName = c.FullName,
                    Party = c.Party,
                    Description = c.Description,
                    Income = c.Income,  // Include Income here
                    Works = c.Works

                }).ToListAsync();

            return View(candidates);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitComplain(ComplaintViewModel model)
        {
            var complaint = new ComplaintsPostDTO
            {
                ElectionID = model.ElectionID,
                ComplaintText = model.ComplaintText,
            };

            if (User.Identity!.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);

                complaint.UserID = userId!;
            }
            var command = new CreateComplaintCommand(complaint);
            await _mediator.Send(command);

            return RedirectToAction("ComplainPage");
        }

        public IActionResult ElectionPage()
        {
            // This is where you'd pull data from the database.

            // For now, we'll mock up some data to illustrate.
            var elections = new List<ElectionsViewModel>
    {
        new ElectionsViewModel { ElectionID = 1, Title = "Presidential Election", Description = "Election to choose the next president.", StartDate = new DateTime(2024, 11, 5), EndDate = new DateTime(2024, 11, 6) },
        new ElectionsViewModel { ElectionID = 2, Title = "Senate Election", Description = "Election to fill senate seats.", StartDate = new DateTime(2024, 11, 5), EndDate = new DateTime(2024, 11, 6) },
        new ElectionsViewModel { ElectionID = 3, Title = "Local Council Election", Description = "Election to elect local council members.", StartDate = new DateTime(2024, 12, 1), EndDate = new DateTime(2024, 12, 2) },
        new ElectionsViewModel { ElectionID = 4, Title = "School Board Election", Description = "Election to elect school board members.", StartDate = new DateTime(2024, 12, 10), EndDate = new DateTime(2024, 12, 11) },
        new ElectionsViewModel { ElectionID = 5, Title = "Mayoral Election", Description = "Election to choose the next mayor.", StartDate = new DateTime(2024, 11, 20), EndDate = new DateTime(2024, 11, 21) }
    };

            return View(elections);
        }

        public async Task<IActionResult> ComplainPage()
        {
            var allElections = await _mediator.Send(new GetAllElectionsQuery());
            var elections = allElections.Select(e => new SelectListItem
            {
                Value = e.ElectionID.ToString(),
                Text = e.Title
            });

            var complainViewModel = new ComplainViewModel
            {
                ElectionID = 0,  // Set a default value or fetch available elections
                ComplaintText = string.Empty,
                Elections = elections,
            };

            return View(complainViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RepliedComplaintsPage()
        {
            // Fetch replied complaints directly from the database using EF Core
            var repliedComplaints = await _context.RepliedComplaints
                .Select(rc => new RepliedComplaintsViewModel
                {
                    RepliedComplaintID = rc.RepliedComplaintID,
                    ComplaintID = rc.ComplaintID,
                    ComplaintText = rc.Complaint.ComplaintText, // Assuming a relationship exists with Complaint
                    ReplyText = rc.ReplyText,
                    ReplyDate = rc.ReplyDate
                })
                .ToListAsync();

            return View(repliedComplaints);  // Passing the data to the view
        }

        public IActionResult VotePage(int electionId)
        {
            // Mock data for candidates belonging to the specific election
            var candidates = new List<CandidateViewModel>
    {
        new CandidateViewModel { CandidateID = 1, ElectionID = electionId, FullName = "John Doe", Party = "Independent", Description = "Lorem ipsum dolor sit amet.", Income = 50000, Works = "Public services" },
        new CandidateViewModel { CandidateID = 2, ElectionID = electionId, FullName = "Jane Smith", Party = "Democrat", Description = "Consectetur adipiscing elit.", Income = 70000, Works = "Health care" }
    };

            // Pass the candidates to the view
            return View(candidates);
        }
    }
}