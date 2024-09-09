using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.ResultViewModels;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;

namespace OnlineVotingS.API.Controllers.TempControllers;

public class ResultController : Controller
{
    private readonly IElectionRepository _electionRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IVotesRepository _votesRepository;
    private readonly IResultRepository _resultRepository;

    public ResultController(IElectionRepository electionRepository, ICandidateRepository candidateRepository, IVotesRepository votesRepository, 
        IResultRepository resultRepository)
    {
        _electionRepository = electionRepository;
        _candidateRepository = candidateRepository;
        _votesRepository = votesRepository;
        _resultRepository = resultRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GenerateResult()
    {
        var allElections = await _electionRepository.GetAllAsync();

        var model = new GenerateResultViewModel
        {
            OngoingElections = allElections.ToList(),
        };
        return View("~/Views/Admin/Result/GenerateResult.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> GenerateResult(int SelectedElectionID, int? CandidateId)
    {
        bool resultExists = false;
        var results = await _resultRepository.GetAllAsync();
        var votesForElection = await _votesRepository.GetByElectionIDAsync(SelectedElectionID);
        
        if (CandidateId != null)
        {
            resultExists = results.Any(x=> x.CandidateID == CandidateId && x.ElectionID == SelectedElectionID);
            votesForElection = votesForElection.Where(x => x.CandidateID == CandidateId);
        }
        else
        {
            resultExists = results.Any(x=> x.ElectionID == SelectedElectionID);
        }
        if (!resultExists)
        {
            var votesBasedOnCandidates = votesForElection.GroupBy(x => x.CandidateID);
            foreach (var vote in votesBasedOnCandidates.ToList())
            {
                Result result = new Result()
                {
                    TotalVotes = vote.Count(),
                    CandidateID = vote.Key,
                    ElectionID = SelectedElectionID
                };
                await _resultRepository.AddAsync(result);
            }
        }
        else
        {
            TempData["Message"] = "Results for this election have already been generated.";
            return RedirectToAction("GenerateResult");
        }

        return await ViewResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetCandidatesByElection(int electionId)
    {
        var candidates = await _candidateRepository.GetByElectionIdAsync(electionId);

        return Json(candidates);
    }

    [HttpGet]
    public async Task<IActionResult> ViewResult()
    {
        var model = new List<ViewResultViewModel>();
        var allResults = await _resultRepository.GetAllAsync();

        foreach (var item in allResults)
        {
            var candidate = await _candidateRepository.GetByIdAsync(item.CandidateID);
            var election = await _electionRepository.GetByIdAsync(item.ElectionID);
            model.Add(new ViewResultViewModel
            {
                ResultID = item.ResultID,
                CandidateID = candidate.CandidateID,
                CandidateName = candidate.FullName,
                ElectionID = election.ElectionID,
                ElectionTitle = election.Title,
                TotalVotes = item.TotalVotes
            });
        }
        
        return View("~/Views/Admin/Result/ViewResult.cshtml", model);
    }
}
