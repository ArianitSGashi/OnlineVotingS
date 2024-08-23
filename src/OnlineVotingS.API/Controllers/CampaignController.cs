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

    // GET: api/Campaign
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetAllCampaigns()
    {
        var query = new GetAllCampaignsQuery();
        var campaigns = await _mediator.Send(query);
        return Ok(campaigns);
    }

    // GET: api/Campaign/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Campaign>> GetCampaignById(int id)
    {
        var query = new GetCampaignByIdQuery(id);
        var campaign = await _mediator.Send(query);
        if (campaign == null)
        {
            return NotFound();
        }
        return Ok(campaign);
    }

    // POST: api/Campaign
    [HttpPost]
    public async Task<ActionResult<Campaign>> CreateCampaign([FromBody] CampaignPostDTO campaignDto)
    {
        var command = new CreateCampaignCommand(campaignDto);
        var campaign = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCampaignById), new { id = campaign.CampaignID }, campaign);
    }

    // PUT: api/Campaign/5
    [HttpPut]
    public async Task<IActionResult> UpdateCampaign([FromBody] CampaignPutDTO campaignDto)
    {
        if (campaignDto == null || campaignDto.CampaignID == 0)
        {
            return BadRequest("Campaign ID is required.");
        }

        try
        {
            var updatedCampaign = await _mediator.Send(new UpdateCampaignCommand(campaignDto));
            if (updatedCampaign == null)
            {
                return NotFound($"Campaign with ID {campaignDto.CampaignID} not found.");
            }

            return Ok(updatedCampaign);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE: api/Campaign/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampaign(int id)
    {
        var command = new DeleteCampaignCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}