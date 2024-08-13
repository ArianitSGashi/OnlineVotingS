using Microsoft.AspNetCore.Mvc;

namespace OnlineVotingS.API.Controllers;

public class VoterController : Controller
{
    public IActionResult VoterDashboard()
    {
        return View();
    }
}