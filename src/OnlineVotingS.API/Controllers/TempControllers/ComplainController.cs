using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.API.Models.AdminViewModels.ComplaintViewModels;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Application.Services.Complaint.Requests.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingS.API.Controllers.TempControllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplainController : Controller
    {
        private readonly IMediator _mediator;

        public ComplainController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ViewComplain")]
        public async Task<IActionResult> ViewComplain()
        {
            var query = new GetAllComplaintCommand(); // Make sure the query is correctly defined
            var complaints = await _mediator.Send(query);

            var model = complaints.Select(c => new ComplaintViewModel
            {
                ComplaintID = c.ComplaintID,
                UserID = c.UserID,
                ElectionID = c.ElectionID,
                ComplaintText = c.ComplaintText,
                ComplaintDate = c.ComplaintDate
            }).ToList();

            return View("~/Views/Admin/Complain/ViewComplain.cshtml", model);
        }


        // Reply to Complaint (GET)
        [HttpGet("ReplyComplain")]
        public async Task<IActionResult> ReplyComplain()
        {
            var query = new GetAllComplaintCommand();
            var complaints = await _mediator.Send(query);

            var model = new ReplyComplaintViewModel
            {
                Complaints = complaints.Select(c => new SelectListItem
                {
                    Value = c.ComplaintID.ToString(),
                    Text = c.ComplaintID.ToString()
                }).ToList()
            };

            return View("~/Views/Admin/Complain/ReplyComplain.cshtml", model);
        }

        // Reply to Complaint (POST)
        [HttpPost("ReplyComplain")]
        public async Task<IActionResult> ReplyComplain([FromBody] ComplaintsPutDTO complaintsPut)
        {
            var command = new UpdateComplaintCommand(complaintsPut);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest("Failed to reply to the complaint.");
            }

            return Ok("Reply sent successfully.");
        }
    }
}
