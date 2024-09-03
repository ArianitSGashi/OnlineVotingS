using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineVotingS.API.Controllers.TempControllers;

[Authorize(Policy = "RequireAdminRole")]
public class AdminController : Controller
{
    [Authorize(Policy = "RequireAdminRole")]
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Voter()
    {
        return View();
    }
}
