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

    [HttpPost]
    public async Task<Complaints> Create([FromBody] ComplaintsPostDTO complaintsPost)
    {
        var response = await Mediator.Send(new CreateComplaintCommand(complaintsPost));

        return response;
    }

    [HttpDelete]
    public async Task<bool> Delete([FromQuery] int ComplaintId)
    {
        var response = await Mediator.Send(new DeleteComplaintCommand(ComplaintId));

        return response;
    }

    [HttpPut]
    public async Task<Complaints> Update([FromBody] ComplaintsPutDTO complaintsPut)
    {
        var response = await Mediator.Send(new UpdateComplaintCommand(complaintsPut));

        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaints>> GetAll()
    {
        var response = await Mediator.Send(new GetAllComplaintCommand());

        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaints>> GetAllByDate([FromQuery] DateTime Date)
    {
        var response = await Mediator.Send(new GetByComplaintDateCommand(Date));

        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaints>> GetAllByElectionId([FromQuery] int ElectionId)
    {
        var response = await Mediator.Send(new GetComplaintByElectionIdCommand(ElectionId));

        return response;
    }

    [HttpGet]
    public async Task<Complaints> GetById([FromQuery] int ComplaintId)
    {
        var response = await Mediator.Send(new GetComplaintsByIdCommand(ComplaintId));

        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaints>> GetComplaintsByUserId([FromQuery] string UserId)
    {
        var response = await Mediator.Send(new GetComplaintsByUserIdCommand(UserId));

        return response;
    }
}
