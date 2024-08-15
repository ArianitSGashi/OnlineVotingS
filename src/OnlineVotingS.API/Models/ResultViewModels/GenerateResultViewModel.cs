using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Models.ResultViewModels;
public class GenerateResultViewModel
{
    public int SelectedElectionID { get; set; }
    public int SelectedCandidateID { get; set; }

    public List<SelectListItem> OngoingElections { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Candidates { get; set; } = new List<SelectListItem>();
}

