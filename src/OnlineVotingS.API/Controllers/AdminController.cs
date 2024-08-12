using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.ViewModels;

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
        var viewModel = new AddCandidateViewModel();
        return View("Candidate/AddCandidate", viewModel);
    }

    [HttpPost]
    public IActionResult AddCandidate(AddCandidateViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Handle form submission
        }
        return View(model);
    }

    public IActionResult EditDeleteCandidate()
    {
        var viewModel = new EditDeleteCandidateViewModel();
        return View("Candidate/EditDeleteCandidate", viewModel);
    }

    [HttpPost]
    public IActionResult EditDeleteCandidate(EditDeleteCandidateViewModel model)
    {
        if (model.Action == "view")
        {
            // Handle viewing candidate details
        }
        else if (model.Action == "delete")
        {
            // Handle deleting candidate
        }
        return View(model);
    }

    public IActionResult ViewCandidate()
    {
        var viewModel = new ViewCandidateViewModel();
        return View("Candidate/ViewCandidate", viewModel);
    }

    [HttpPost]
    public IActionResult ViewCandidate(ViewCandidateViewModel model)
    {
        // Handle filtering candidates
        return View(model);
    }

}