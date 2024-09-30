using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.Application.DTO.PutDTO;

public class ComplaintsPutDTO
{
    [Required]
    public int ComplaintID { get; set; }
    public string UserID { get; set; } = null!;
    public int ElectionID { get; set; }

    [Required(ErrorMessage = "Please enter your complaint.")]

    [StringLength(200, ErrorMessage = "The complaint must be at most 200 characters long.")]
    
    [RegularExpression(@"[^+^\-^\/^\*^\(^\)]", ErrorMessage = "Special characters aren't allowed.")] 
    public string ComplaintText { get; set; } = null!;
    public DateTime ComplaintDate { get; set; } = DateTime.UtcNow;
}