using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Models.ElectionViewModels;

public class CompleteElectionViewModel
{
    public int SelectedElectionID { get; set; } 
    public List<SelectListItem> OngoingElections { get; set; } = new List<SelectListItem>();
}
