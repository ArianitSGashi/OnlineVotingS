using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

public class CompleteElectionViewModel
{
    public string SelectedTitle { get; set; } = string.Empty;
    public List<SelectListItem> OngoingElections { get; set; } = new List<SelectListItem>();
}
