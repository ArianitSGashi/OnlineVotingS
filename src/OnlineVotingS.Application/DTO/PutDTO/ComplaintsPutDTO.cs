using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.Application.DTO.PutDTO;

public class ComplaintsPutDTO
{
    [Required]
    public int ComplaintID { get; set; }
    public string UserID { get; set; } = null!;
    public int ElectionID { get; set; }
    public string ComplaintText { get; set; } = null!;
    public DateTime ComplaintDate { get; set; } = DateTime.UtcNow;
}