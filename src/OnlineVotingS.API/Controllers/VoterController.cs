using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models;

namespace OnlineVotingS.API.Controllers;

public class VoterController : Controller
{
    public IActionResult VoterDashboard()
    {
        return View();
    }
    // GET: /Voter/Candidates
    public IActionResult CandidatePage()
    {
        // This is where you'd pull data from the database.
        // For now, we'll mock up some data to illustrate.
        var candidates = new List<CandidateViewModel>
            {
                new CandidateViewModel { CandidateID = 1, ElectionID = 101, FullName = "John Doe", Party = "Independent", Description = "Lorem ipsum dolor sit amet.", Income = 50000, Works = "Public services" },
                new CandidateViewModel { CandidateID = 2, ElectionID = 101, FullName = "Jane Smith", Party = "Democrat", Description = "Consectetur adipiscing elit.", Income = 70000, Works = "Health care" },
            };

        return View(candidates);
    }
}

