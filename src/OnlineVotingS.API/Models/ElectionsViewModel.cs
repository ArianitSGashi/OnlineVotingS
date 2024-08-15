using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models;

   public class ElectionsViewModel
    {
     public int ElectionID { get; set; }
     public string Title { get; set; } = string.Empty;
     public string? Description { get; set; }
     public DateTime StartDate { get; set; }
     public DateTime EndDate { get; set; }

    // New property to check if the user has already voted in this election
    public bool UserHasVoted { get; set; }

    // New property to check if the election is completed
    public bool IsCompleted { get; set; }
}
