using System;
using System.ComponentModel.DataAnnotations;
namespace OnlineVotingS.API.Models.ElectionViewModels;
public class GenerateElectionViewModel
{
    public int ElectionID { get; set; } 
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "End date is required")]
    public DateTime EndDate { get; set; }
}
