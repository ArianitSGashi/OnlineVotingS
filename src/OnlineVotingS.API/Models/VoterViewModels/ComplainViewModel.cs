using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.VoterViewModels;

public class ComplainViewModel
{
    public int ComplaintID { get; set; }

    public string UserID { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select an election.")]
    public int ElectionID { get; set; }

    [Required(ErrorMessage = "Please enter your complaint.")]
    [StringLength(200, ErrorMessage = "The complaint must be at most 200 characters long.")]
    [RegularExpression(@"^[^+*/@]+$", ErrorMessage = "Special characters aren't allowed.")]
    public string ComplaintText { get; set; } = string.Empty;

    public DateTime ComplaintDate { get; set; }

    public IEnumerable<SelectListItem> Elections { get; set; } = new List<SelectListItem>();
}