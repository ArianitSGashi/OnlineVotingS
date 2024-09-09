using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

public class DeleteElectionViewModel
{
    [Required]
    public int ElectionID { get; set; }
}
