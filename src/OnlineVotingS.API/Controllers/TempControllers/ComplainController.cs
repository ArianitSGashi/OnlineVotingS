using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.API.Models.AdminViewModels.ComplaintViewModels;
using MediatR;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class ComplainController : Controller
{
    private readonly ISender Mediator;
    public ComplainController(ISender _mediator)
    {
        Mediator = _mediator;
    }

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
