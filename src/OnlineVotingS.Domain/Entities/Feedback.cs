using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVotingS.Domain.Models;

namespace OnlineVotingS.Domain.Entities;

public class Feedback
{
    /// <summary>
    /// Unique identifier for the feedback.
    /// </summary>
    [Key]
    public int FeedbackID { get; set; }
    /// <summary>
    /// Unique identifier for the user providing the feedback.
    /// </summary>
    public string UserID { get; set; } = null!;
    /// <summary>
    /// Unique identifier for the election associated with the feedback.
    /// </summary>
    [ForeignKey("Elections")]
    public int ElectionID { get; set; }
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
    /// Election object associated with the feedback, representing the election details.
    /// </summary>
    public Elections Elections { get; set; } = null!;
    /// <summary>
    /// Navigation property for IdentityUser
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
