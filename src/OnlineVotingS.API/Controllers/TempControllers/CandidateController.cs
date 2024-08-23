using System.Collections.Generic;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.API.Models.AdminViewModels.ComplaintViewModels;
using MediatR;
using OnlineVotingS.Application.Services.Candidate.Requests.Commands;
using OnlineVotingS.Application.Services.Candidate.Requests.Queries;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using Microsoft.AspNetCore.Mvc;


namespace OnlineVotingS.API.Controllers.TempControllers;

public class CandidateController : Controller
{
    private readonly ISender _mediator;

    public CandidateController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Candidates> Create([FromBody] CandidatesPostDTO candidatesPost)
    {
        var response = await _mediator.Send(new CreateCandidateCommand(candidatesPost));
        return response;
    }

    [HttpDelete]
    public async Task<bool> Delete(int candidateId)
    {
        var response = await _mediator.Send(new DeleteCandidateCommand(candidateId));
        return response;
    }

    [HttpPut]
    public async Task<Candidates> Update([FromBody] CandidatesPutDTO candidatesPut)
    {
        var response = await _mediator.Send(new UpdateCandidateCommand(candidatesPut));
        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Candidates>> GetAll()
    {
        var response = await _mediator.Send(new GetAllCandidatesQuery());
        return response;
    }

    [HttpGet]
    public async Task<Candidates> GetById([FromQuery] int candidateId)
    {
        var response = await _mediator.Send(new GetCandidateByIdQuery(candidateId));
        return response;
    }

    [HttpGet]
    public async Task<IEnumerable<Candidates>> GetByElectionId([FromQuery] int electionId)
    {
        var response = await _mediator.Send(new GetCandidatesByElectionIdQuery(electionId));
        return response;
    }
}