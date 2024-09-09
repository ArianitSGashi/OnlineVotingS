using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

public class ModifyElectionViewModel
{
    public int ElectionID { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Start date is required")]
    public DateOnly StartDate { get; set; }
    [Required(ErrorMessage = "Start time is required")]
    public TimeSpan StartTime { get; set; }
    [Required(ErrorMessage = "End date is required")]
    public DateOnly EndDate { get; set; }
    [Required(ErrorMessage = "End time is required")]
    public TimeSpan EndTime { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
