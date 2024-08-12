using Microsoft.AspNetCore.Mvc;

namespace OnlineVotingS.API.Controllers;

public class AdminController : Controller
{
   
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Voter()
    {
        return View();
    }
    public IActionResult Footer()
    {
        return View();
    }
    public IActionResult AddCandidate()
    {
        return View("Candidate/AddCandidate");
    }

    public IActionResult EditDeleteCandidate()
    {
        return View("Candidate/EditDeleteCandidate");
    }

    public IActionResult ViewCandidate()
    {
        return View("Candidate/ViewCandidate");
    }
}
