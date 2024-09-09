using System.ComponentModel.DataAnnotations;
using OnlineVotingS.Domain.Enums;

namespace OnlineVotingS.Domain.Entities;

public class Elections
{
    /// <summary>
    /// Gets or sets the unique identifier for the election.
    /// </summary>
    [Key]
    public int ElectionID { get; set; }
    /// <summary>
    /// Gets or sets the title of the election.
    /// </summary>
    [Required] [MaxLength(50)]
    public string Title { get; set; } = null!;
    /// <summary>
    /// Gets or sets the description of the election.
    /// </summary>
    [MaxLength(100)]
    public string? Description { get; set; }
    /// <summary>
    /// Gets or sets the start date of the election.
    /// </summary>
    public DateOnly StartDate { get; set; }
    /// <summary>
    /// Gets or sets the start time of the election.
    /// </summary>
    public TimeSpan StartTime { get; set; }
    /// <summary>
    /// Gets or sets the end date of the election.
    /// </summary>
    public DateOnly EndDate { get; set; }
    /// <summary>
    /// Gets or sets the end time of the election.
    /// </summary>
    public TimeSpan EndTime { get; set; }
    /// <summary>
    /// Gets or sets the status of the election.
    /// </summary>
    public ElectionStatus Status { get; set; } = ElectionStatus.Active;
    /// <summary>
    /// Gets or sets the date and time when the election was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Gets or sets the date and time when the election was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    public ICollection<Candidates> Candidates { get; set; } = new List<Candidates>();
    public ICollection<Votes> Votes { get; set; } = new List<Votes>();
    public ICollection<Result> Results { get; set; } = new List<Result>();
    public ICollection<Complaints> Complaints { get; set; } = new List<Complaints>();
    public ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
