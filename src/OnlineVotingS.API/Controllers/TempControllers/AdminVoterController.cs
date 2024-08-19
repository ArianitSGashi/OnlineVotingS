using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.AdminVoterViewModels;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class AdminVoterController : Controller
{
    public IActionResult AddVoter()
    {
        return View("~/Views/Admin/AdminVoter/AddVoter.cshtml");
    }

    public IActionResult DeleteVoter()
    {
        return View("~/Views/Admin/AdminVoter/DeleteVoter.cshtml");
    }

    public IActionResult EditVoter()
    {
        return View("~/Views/Admin/AdminVoter/EditVoter.cshtml");
    }

    public IActionResult ViewVoters()
    {
        var model = new List<ViewVoterViewModel>();
        // You would normally get this data from your database

        return View("~/Views/Admin/AdminVoter/ViewVoters.cshtml", model);
    }
}
