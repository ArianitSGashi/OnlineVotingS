using System.ComponentModel.DataAnnotations;
namespace OnlineVotingS.API.Models.ElectionViewModels;
public class DeleteElectionViewModel
{
    [Required]
    public string ElectionID { get; set; } = string.Empty;
}
