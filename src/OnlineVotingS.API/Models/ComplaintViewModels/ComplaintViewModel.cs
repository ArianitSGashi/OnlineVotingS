using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.ComplaintViewModels;

    public class ComplaintViewModel
    {
        public int ComplaintID { get; set; }

        [Required]
        public string UserID { get; set; } = string.Empty;

        [Required]
        public int ElectionID { get; set; }

        public string ElectionTitle { get; set; } = string.Empty;

        [Required]
        [MaxLength(200, ErrorMessage = "The complaint text cannot exceed 200 characters.")]
        public string ComplaintText { get; set; } = string.Empty;

        [Required]
        public DateTime ComplaintDate { get; set; }
    }