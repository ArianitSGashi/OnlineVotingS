using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;
using OnlineVotingS.API.Models.AdminViewModels.AdminVoterViewModels;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class CandidateController : Controller
{
    [HttpGet]
    public IActionResult AddCandidate()
    {
        var model = new AddCandidateViewModel
        {
            Elections = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "hi" },
                    new SelectListItem { Value = "2", Text = "," },
                    new SelectListItem { Value = "3", Text = "." }
                }
        };
        return View("~/Views/Admin/Candidate/AddCandidate.cshtml", model);
    }

    public IActionResult EditCandidate()
    {
        return View("~/Views/Admin/Candidate/EditCandidate.cshtml");
    }

    public IActionResult DeleteCandidate()
    {
        return View("~/Views/Admin/Candidate/DeleteCandidate.cshtml");
    }

    public IActionResult ViewCandidates()
    {
        var model = new List<ViewCandidatesViewModel>();
        return View("~/Views/Admin/Candidate/ViewCandidates.cshtml", model);
    }
}