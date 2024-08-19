using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;

public class DeleteCandidateViewModel
{
    [Required]
    public string CandidateID { get; set; } = string.Empty;
}
