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

    /// <summary>
    /// Retrieves all campaigns.
    /// </summary>
    /// <returns>A list of campaigns.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetAllCampaigns()
    {
        var query = new GetAllCampaignsQuery();
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    /// <summary>
    /// Retrieves a campaign by ID.
    /// </summary>
    /// <param name="id">The ID of the campaign.</param>
    /// <returns>The campaign with the specified ID.</returns>
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

    /// <summary>
    /// Retrieves active campaigns.
    /// </summary>
    /// <returns>A list of active campaigns.</returns>
    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetActiveCampaigns()
    {
        var query = new GetActiveCampaignsQuery();
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    /// <summary>
    /// Retrieves campaigns by candidate ID.
    /// </summary>
    /// <param name="candidateId">The ID of the candidate.</param>
    /// <returns>A list of campaigns for the specified candidate.</returns>
    [HttpGet("candidate/{candidateId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaignsByCandidateId([FromRoute] int candidateId)
    {
        var query = new GetCampaignsByCandidateIdQuery(candidateId);
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    /// <summary>
    /// Retrieves campaigns by election ID.
    /// </summary>
    /// <param name="electionId">The ID of the election.</param>
    /// <returns>A list of campaigns for the specified election.</returns>
    [HttpGet("election/{electionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaignsByElectionId([FromRoute] int electionId)
    {
        var query = new GetCampaignsByElectionIdQuery(electionId);
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    /// <summary>
    /// Creates a new campaign.
    /// </summary>
    /// <param name="campaignDto">The campaign data.</param>
    /// <returns>The created campaign.</returns>
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

    /// <summary>
    /// Updates an existing campaign.
    /// </summary>
    /// <param name="campaignDto">The updated campaign data.</param>
    /// <returns>No content if successful, or appropriate error response.</returns>
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

    /// <summary>
    /// Deletes a campaign by ID.
    /// </summary>
    /// <param name="id">The ID of the campaign.</param>
    /// <returns>No content if successful, or appropriate error response.</returns>
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