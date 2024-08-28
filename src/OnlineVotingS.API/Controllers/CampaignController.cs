using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Campaigns.Requests.Commands;
using OnlineVotingS.Application.Services.Campaigns.Requests.Queries;
using OnlineVotingS.Domain.Entities;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetAllCampaigns()
    {
        var query = new GetAllCampaignsQuery();
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Campaign>> GetCampaignById([FromRoute] int id)
    {
        var query = new GetCampaignByIdQuery(id);
        var campaign = await _mediator.Send(query);
        return campaign == null
            ? NotFound($"Campaign with ID {id} not found.")
            : Ok(campaign);
    }

    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetActiveCampaigns()
    {
        var query = new GetActiveCampaignsQuery();
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    [HttpGet("candidate/{candidateId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaignsByCandidateId([FromRoute] int candidateId)
    {
        var query = new GetCampaignsByCandidateIdQuery(candidateId);
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    [HttpGet("election/{electionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaignsByElectionId([FromRoute] int electionId)
    {
        var query = new GetCampaignsByElectionIdQuery(electionId);
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Campaign>> CreateCampaign([FromBody] CampaignPostDTO campaignDto)
    {
        if (campaignDto == null)
        {
            return BadRequest("Campaign data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new CreateCampaignCommand(campaignDto);
        var campaign = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCampaignById), new { id = campaign.CampaignID }, campaign);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCampaign([FromBody] CampaignPutDTO campaignDto)
    {
        if (campaignDto == null || campaignDto.CampaignID <= 0)
        {
            return BadRequest("Valid Campaign ID is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdateCampaignCommand(campaignDto);
        var updatedCampaign = await _mediator.Send(command);

        return updatedCampaign == null
            ? NotFound($"Campaign with ID {campaignDto.CampaignID} not found.")
            : NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCampaign([FromRoute] int id)
    {
        var command = new DeleteCampaignCommand(id);
        var result = await _mediator.Send(command);
        return result
            ? NoContent()
            : NotFound($"Campaign with ID {id} not found.");
    }
}