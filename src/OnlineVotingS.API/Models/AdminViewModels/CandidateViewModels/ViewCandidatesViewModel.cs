using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;

public class ViewCandidatesViewModel
{
    public int CandidateID { get; set; }
    public int ElectionID { get; set; }

    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;
    public string? Party { get; set; } = string.Empty;
    public string? Description { get; set; }

    [DataType(DataType.Currency)]
    public decimal? Income { get; set; }
    public string? Works { get; set; } = string.Empty;
}