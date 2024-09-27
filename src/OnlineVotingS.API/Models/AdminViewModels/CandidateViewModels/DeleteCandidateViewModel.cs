using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;

public class DeleteCandidateViewModel
{
    [Required(ErrorMessage = "Please enter a candidate ID to delete")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid positive integer for the Candidate ID")]
    public int CandidateID { get; set; }
}
