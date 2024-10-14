using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.VoterViewModels;
using OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

namespace OnlineVotingS.API.Controllers.TempControllers;

[Authorize(Policy = "RequireAdminRole")]
public class FeedbackController : Controller
{
    private readonly IMediator _mediator;

    public FeedbackController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ViewFeedbacks()
    {
        var result = await _mediator.Send(new GetAllFeedbacksQuery());

        if (result.IsFailed)
        {
            return HandleErrorResult(result);
        }

        var viewModel = result.Value.Select(f => new FeedbackViewModel
        {
            FeedbackID = f.FeedbackID,
            FeedbackText = f.FeedbackText,
            FeedbackCategory = f.FeedbackCategory,
            FeedbackDate = f.FeedbackDate
        }).ToList();

        return View("~/Views/Admin/Feedback/ViewFeedbacks.cshtml", viewModel);
    }

    private IActionResult HandleErrorResult<T>(Result<T> result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Message);
        }

        return View("Error"); // Render a generic error view
    }
}
