using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;

public class EditCandidateViewModel
{
    [Required(ErrorMessage = "Candidate ID is required")]
    public int CandidateID { get; set; }

    [Required(ErrorMessage = "Please select an election")]
    public int ElectionID { get; set; }

    [Required(ErrorMessage = "Full name is required")]
    [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Party is required")]
    [MaxLength(50, ErrorMessage = "Party name cannot exceed 50 characters")]
    public string? Party { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Income is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Income must be a positive number")]
    public decimal? Income { get; set; }

    [Required(ErrorMessage = "Works information is required")]
    [MaxLength(100, ErrorMessage = "Works information cannot exceed 100 characters")]
    public string? Works { get; set; } = string.Empty;

    [Required(ErrorMessage = "Candidate list is required")]
    public IEnumerable<SelectListItem> CandidateList { get; set; } = new List<SelectListItem>();
}