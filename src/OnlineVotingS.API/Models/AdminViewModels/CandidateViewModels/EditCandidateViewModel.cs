using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;

public class EditCandidateViewModel
{
    public string CandidateID { get; set; } = string.Empty;
    [Required]
    public string ElectionID { get; set; } = string.Empty;
    [Required]
    public string FullName { get; set; } = string.Empty;
    [Required]
    public string Party { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? Description { get; set; }
    [Required]
    public decimal Income { get; set; }
    [Required]
    public string Works { get; set; } = string.Empty;
    [Required]
    public string SelectedElectionID { get; set; } = string.Empty;
    public IEnumerable<SelectListItem> Elections { get; set; } = new List<SelectListItem>();
}
