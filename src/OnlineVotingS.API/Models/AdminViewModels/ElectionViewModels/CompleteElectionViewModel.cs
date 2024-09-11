using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

public class CompleteElectionViewModel
{
    [Required(ErrorMessage = "Please select an election to complete")]
    public string SelectedTitle { get; set; } = string.Empty;
    public List<SelectListItem> OngoingElections { get; set; } = new List<SelectListItem>();
}