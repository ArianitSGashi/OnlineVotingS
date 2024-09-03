using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.GuestViewModels;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class GuestController : Controller
{
    public IActionResult GuestDashboard()
    {
        return View();
    }

    public IActionResult CandidatePage()
    {
        // This is where you'd pull data from the database.

        // For now, we'll mock up some data to illustrate.
        var candidates = new List<CandidateViewModel>
    {
        new CandidateViewModel { CandidateID = 1, ElectionID = 101, FullName = "John Doe", Party = "Independent", Description = "Lorem ipsum dolor sit amet.", Income = 50000, Works = "Public services" },
        new CandidateViewModel { CandidateID = 2, ElectionID = 101, FullName = "Jane Smith", Party = "Democrat", Description = "Consectetur adipiscing elit.", Income = 70000, Works = "Health care" },
        new CandidateViewModel { CandidateID = 1, ElectionID = 101, FullName = "John Doe", Party = "Independent", Description = "Lorem ipsum dolor sit amet.", Income = 50000, Works = "Public services" },
        new CandidateViewModel { CandidateID = 2, ElectionID = 101, FullName = "Jane Smith", Party = "Democrat", Description = "Consectetur adipiscing elit.", Income = 70000, Works = "Health care" },
        new CandidateViewModel { CandidateID = 1, ElectionID = 101, FullName = "John Doe", Party = "Independent", Description = "Lorem ipsum dolor sit amet.", Income = 50000, Works = "Public services" },
        new CandidateViewModel { CandidateID = 2, ElectionID = 101, FullName = "Jane Smith", Party = "Democrat", Description = "Consectetur adipiscing elit.", Income = 70000, Works = "Health care" },
    };

        return View(candidates);
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
}
