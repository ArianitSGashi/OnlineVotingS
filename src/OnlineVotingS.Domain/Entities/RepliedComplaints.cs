using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineVotingS.Domain.Entities;

public class RepliedComplaints
{
    /// <summary>
    /// Gets or sets the unique identifier for the replied complaint.
    /// </summary>
    [Key]
    public int RepliedComplaintID { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier of the complaint related to this reply.
    /// </summary>
    [ForeignKey("Complaints")]
    public int ComplaintID { get; set; }
    /// <summary>
    /// Gets or sets the text of the reply.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string ReplyText { get; set; } = null!;
    /// <summary>
    /// Gets or sets the date the reply was made.
    /// </summary>
    public DateTime ReplyDate { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Navigation property to the associated Complaint entity.
    /// </summary>
    [JsonIgnore]
    public Complaints Complaint { get; set; } = null!;
}