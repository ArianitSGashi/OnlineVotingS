using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.Application.DTO.PostDTO;

public class ComplaintsPostDTO
{
    public string UserID { get; set; } = null!;
    [Required]
    public int ElectionID { get; set; }
    public string ComplaintText { get; set; } = null!;
    [Required]
    public DateTime ComplaintDate { get; set; } = DateTime.Now;
}