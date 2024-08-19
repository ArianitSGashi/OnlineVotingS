using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

public class DeleteElectionViewModel
{
    [Required]
    public string ElectionID { get; set; } = string.Empty;
}
