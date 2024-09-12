using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Models.AdminViewModels.ResultViewModels;

public class GenerateResultViewModel
{
    public int SelectedElectionID { get; set; }
    public int SelectedCandidateID { get; set; }

    public List<Elections> OngoingElections { get; set; } = new List<Elections>();
    public List<SelectListItem> Candidates { get; set; } = new List<SelectListItem>();
}

