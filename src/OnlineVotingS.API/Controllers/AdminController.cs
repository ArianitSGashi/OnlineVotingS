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

}
