using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

public class DeleteElectionViewModel
{
    [Required(ErrorMessage = "Please select an election to delete")]
    public int SelectedElectionID { get; set; }
    public List<SelectListItem> AvailableElections { get; set; } = new List<SelectListItem>();
}