using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// Gets or sets the start date and time of the election.
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Gets or sets the end date and time of the election.
    /// </summary>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the election was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Gets or sets the date and time when the election was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Candidates> Candidates { get; set; } = new List<Candidates>();
    public ICollection<Votes> Votes { get; set; } = new List<Votes>();
    public ICollection<Result> Results { get; set; } = new List<Result>();
    public ICollection<Complaints> Complaints { get; set; } = new List<Complaints>();
    public ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
