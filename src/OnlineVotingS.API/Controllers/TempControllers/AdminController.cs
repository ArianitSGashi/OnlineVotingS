using Microsoft.AspNetCore.Mvc;

namespace OnlineVotingS.API.Controllers.TempControllers;

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
}
