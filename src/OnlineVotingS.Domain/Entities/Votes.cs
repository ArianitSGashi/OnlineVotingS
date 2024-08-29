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

public class Votes
{
    /// <summary>
    /// Unique identifier for the vote.
    /// </summary>
    [Key]
    public int VoteID { get; set; }
    /// <summary>
    /// Unique identifier for the user who cast the vote.
    /// </summary>
    public string UserID { get; set; } = null!;
    /// <summary>
    /// Unique identifier for the election in which the vote was cast.
    /// </summary>
    [ForeignKey("Elections")]
    public int ElectionID { get; set; }
    /// <summary>
    /// Unique identifier for the candidate who received the vote.
    /// </summary>
    [ForeignKey("Candidates")]
    public int CandidateID { get; set; }
    /// <summary>
    /// Date and time when the vote was cast.
    /// </summary>
    public DateTime VoteDate { get; set; }
    /// <summary>
    /// Candidate object associated with the vote, representing the candidate details.
    /// </summary>
    [JsonIgnore]
    public Candidates Candidates { get; set; } = null!;
    /// <summary>
    /// Election object associated with the vote, representing the election details.
    /// </summary>
    [JsonIgnore]
    public Elections Elections { get; set; } = null!;
    /// <summary>
    /// Navigation property for IdentityUser
    /// </summary>
    [JsonIgnore]
    public ApplicationUser User { get; set; } = null!;
}
