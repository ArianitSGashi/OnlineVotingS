using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.ComplaintViewModels;

public class ReplyComplaintViewModel
{
    [Required]
    public int ComplaintID { get; set; }

    public IEnumerable<SelectListItem> Complaints { get; set; } = new List<SelectListItem>();

    [Required]
    [MaxLength(200, ErrorMessage = "The reply text cannot exceed 200 characters.")]
    public string ReplyText { get; set; } = string.Empty;
    public DateTime ReplyDate { get; set; } = DateTime.UtcNow;

}