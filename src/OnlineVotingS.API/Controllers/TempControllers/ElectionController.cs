using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class ElectionController : Controller
{
    public IActionResult GenerateElection()
    {
        return View("~/Views/Admin/Election/GenerateElection.cshtml");
    }

    public IActionResult ModifyElection()
    {
        return View("~/Views/Admin/Election/ModifyElection.cshtml");
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

    public IActionResult DeleteElection()
    {
        return View("~/Views/Admin/Election/DeleteElection.cshtml");
    }

    public IActionResult ViewElection()
    {
        var model = new List<ViewElectionViewModel>();
        return View("~/Views/Admin/Election/ViewElection.cshtml", model);
    }
}
