using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models.AdminViewModels.ComplaintViewModels;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class ComplainController : Controller
{
    private readonly IMediator _mediator;

    public ComplainController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ViewComplain()
    {
        var query = new GetAllComplaintCommand();
        var result = await _mediator.Send(query);
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var model = result.Value.Select(c => new ComplaintViewModel
        {
            ComplaintID = c.ComplaintID,
            UserID = c.UserID,
            ElectionID = c.ElectionID,
            ComplaintText = c.ComplaintText,
            ComplaintDate = c.ComplaintDate
        }).ToList();

        return View("~/Views/Admin/Complain/ViewComplain.cshtml", model);
    }

    [HttpGet]
    public async Task<IActionResult> ReplyComplain()
    {
        var query = new GetAllComplaintCommand();
        var result = await _mediator.Send(query);
        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var model = new ReplyComplaintViewModel
        {
            Complaints = result.Value.Select(c => new SelectListItem
            {
                Value = c.ComplaintID.ToString(),
                Text = c.ComplaintID.ToString()
            }).ToList()
        };

        return View("~/Views/Admin/Complain/ReplyComplain.cshtml", model);
    }

    [HttpPost("ReplyComplain")]
    public async Task<IActionResult> ReplyComplain([FromBody] ComplaintsPutDTO complaintsPut)
    {
        var command = new UpdateComplaintCommand(complaintsPut);
        var result = await _mediator.Send(command);

        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        return Ok("Reply sent successfully.");
    }

    private IActionResult HandleErrorResult<T>(Result<T> result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Message);
        }

        return View("Error");
    }
}