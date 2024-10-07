using System.ComponentModel.DataAnnotations;
using OnlineVotingS.Domain.Enums;

namespace OnlineVotingS.Domain.Entities;

public class Feedback
{
    /// <summary>
    /// Unique identifier for the feedback.
    /// </summary>
    [Key]
    public int FeedbackID { get; set; }
    /// <summary>
    /// Text content of the feedback provided by the user.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FeedbackText { get; set; } = null!;
    /// <summary>
    /// Date and time when the feedback was provided.
    /// </summary>
    public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Category of the Feedback
    /// </summary>
    public FeedbackCategory FeedbackCategory { get; set; }
}