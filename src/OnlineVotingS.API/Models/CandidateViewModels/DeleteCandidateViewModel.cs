using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminCandidateViewModels;

public class DeleteCandidateViewModel
{
    [Required]
    public string CandidateID { get; set; } = string.Empty;
}
