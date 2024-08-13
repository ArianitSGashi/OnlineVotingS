using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models;


namespace YourNamespace.Controllers
{
    public class ElectionController : Controller
    {
        public IActionResult GenerateElection()
        {
            return View("~/Views/Admin/Election/GenerateElection.cshtml"); // Full path to the view
        }

        public IActionResult CompleteElection()
        {
            var ongoingElections = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Election 1" },
                new SelectListItem { Value = "2", Text = "Election 2" }
            };

            var model = new CompleteElectionViewModel
            {
                OngoingElections = ongoingElections
            };

            return View("~/Views/Admin/Election/CompleteElection.cshtml", model);
        }
      
    }
}
