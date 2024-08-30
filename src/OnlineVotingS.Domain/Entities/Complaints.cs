using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVotingS.Domain.Models;
using System.Text.Json.Serialization;

namespace OnlineVotingS.Domain.Entities;

public class Complaints
{
    /// <summary>
    /// Gets or sets the unique identifier for the complaint.
    /// </summary>
    [Key]
    public int ComplaintID { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier of the user who filed the complaint.
    /// </summary>
    public string UserID { get; set; } = null!;
    /// <summary>
    /// Gets or sets the unique identifier of the election related to the complaint.
    /// </summary>
    [ForeignKey("Elections")]
    public int ElectionID { get; set; }
    /// <summary>
    /// Gets or sets the text of the complaint.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string ComplaintText { get; set; } = null!;
    /// <summary>
    /// Gets or sets the date the complaint was filed.
    /// </summary>
    public DateTime ComplaintDate { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Navigation property to the associated Election entity.
    /// </summary>
    [JsonIgnore]
    public Elections Elections { get; set; } = null!;
    /// <summary>
    /// Navigation property for IdentityUser
    /// </summary>
    [JsonIgnore]
    public ApplicationUser User { get; set; } = null!;
    /// <summary>
    /// Navigation property to RepliedComplaints
    /// <summary>
    public ICollection<RepliedComplaints> RepliedComplaints { get; set; } = new List<RepliedComplaints>();
}
