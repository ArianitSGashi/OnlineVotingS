using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models;

namespace OnlineVotingS.API.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult ShowResult()
        {
            var electionList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Election 1", Value = "1" },
                new SelectListItem { Text = "Election 2", Value = "2" }
            };

            var candidateList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Candidate A", Value = "1" },
                new SelectListItem { Text = "Candidate B", Value = "2" }
            };

            ViewBag.ElectionList = new SelectList(electionList, "Value", "Text");
            ViewBag.CandidateList = new SelectList(candidateList, "Value", "Text");

            return View("~/Views/Admin/Result/ShowResult.cshtml"); // Full path to the view
        }
    }
}
