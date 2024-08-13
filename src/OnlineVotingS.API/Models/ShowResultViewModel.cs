using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models;

public class ShowResultViewModel
{
    public int ResultID { get; set; }

    [Required(ErrorMessage = "Election ID is required")]
    public string ElectionID { get; set; } = string.Empty;

    [Required(ErrorMessage = "Candidate ID is required")]
    public int CandidateID { get; set; }

    [Required(ErrorMessage = "Total votes are required")]
    public int TotalVotes { get; set; }
}
