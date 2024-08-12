using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OnlineVotingS.API.Models.ComplaintViewModels;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Controllers
{
    public class ComplainController : Controller
    {
        [HttpGet]
        public IActionResult ViewComplain()
        {
            // This should match the path in your project
            var complaints = new List<ComplaintViewModel>(); // Dummy data for now
            return View("~/Views/Admin/Complain/ViewComplain.cshtml", complaints);
        }

        [HttpGet]
        public IActionResult ReplyComplain()
        {
            // This should match the path in your project
            var model = new ReplyComplaintViewModel(); // Dummy data for now
            return View("~/Views/Admin/Complain/ReplyComplain.cshtml", model);
        }
    }
}
