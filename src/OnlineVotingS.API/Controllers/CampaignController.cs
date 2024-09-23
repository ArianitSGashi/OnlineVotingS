using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;

namespace OnlineVotingS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignController : ControllerBase
{
    private readonly IMediator _mediator;

    public CampaignController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCampaignsAsync()
    {
        var query = new GetAllCampaignsQuery();
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCampaignByIdAsync(int id)
    {
        var query = new GetCampaignByIdQuery(id);
        var campaign = await _mediator.Send(query);
        return Ok(campaign);
    }

    [HttpGet("candidate/{candidateId}")]
    public async Task<IActionResult> GetCampaignsByCandidateIdAsync(int candidateId)
    {
        var query = new GetCampaignsByCandidateIdQuery(candidateId);
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    [HttpGet("election/{electionId}")]
    public async Task<IActionResult> GetCampaignsByElectionIdAsync(int electionId)
    {
        var query = new GetCampaignsByElectionIdQuery(electionId);
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveCampaignsAsync()
    {
        var query = new GetActiveCampaignsQuery();
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCampaignAsync([FromBody] CampaignPostDTO campaignDto)
    {
        var command = new CreateCampaignCommand(campaignDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var campaign = result.Value;
            return CreatedAtAction(nameof(GetCampaignByIdAsync), new { id = campaign.CampaignID }, campaign);
        }
        return BadRequest(result.Errors);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCampaignAsync([FromBody] CampaignPutDTO campaignDto)
    {
        var command = new UpdateCampaignCommand(campaignDto);
        var updatedCampaign = await _mediator.Send(command);
        return Ok(updatedCampaign);
    }

    [HttpDelete("{id?}")]
    public async Task<IActionResult> DeleteCampaignAsync(int id)
    {
        var command = new DeleteCampaignCommand(id);
        var result = await _mediator.Send(command);
        return NoContent();
    }
}