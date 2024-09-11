using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

public class ModifyElectionViewModel : IElectionViewModel
{
    [Required(ErrorMessage = "Please select an election to modify")]
    public int ElectionID { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Start date is required")]
    [DataType(DataType.Date)]
    public DateOnly? StartDate { get; set; }

    [Required(ErrorMessage = "Start time is required")]
    [DataType(DataType.Time)]
    public TimeSpan? StartTime { get; set; }

    [Required(ErrorMessage = "End date is required")]
    [DataType(DataType.Date)]
    public DateOnly? EndDate { get; set; }

    [Required(ErrorMessage = "End time is required")]
    [DataType(DataType.Time)]
    public TimeSpan? EndTime { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}