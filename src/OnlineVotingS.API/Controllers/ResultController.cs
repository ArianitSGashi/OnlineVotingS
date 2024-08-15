using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.ResultViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Controllers;
public class ResultController : Controller
{
    [HttpGet]
    public IActionResult GenerateResult()
    {
        var model = new GenerateResultViewModel
        {
            OngoingElections = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Election 1" },
                new SelectListItem { Value = "2", Text = "Election 2" }
            }
        };
        return View("~/Views/Admin/Result/GenerateResult.cshtml", model);
    }
    [HttpGet]
    public IActionResult ViewResult()
    {
        var model = new List<ViewResultViewModel>
        {
            new ViewResultViewModel
            {
                ResultID = 1,
                ElectionID = 1,
                ElectionTitle = "Election 1",
                CandidateID = 101,
                CandidateName = "Candidate A",
                TotalVotes = 5000
            }
        };
        return View("~/Views/Admin/Result/ViewResult.cshtml", model);
    }
}
