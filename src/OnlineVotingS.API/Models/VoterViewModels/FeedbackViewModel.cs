using OnlineVotingS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.VoterViewModels
{
    public class FeedbackViewModel
    {
        public int FeedbackID { get; set; }

        [Required(ErrorMessage = "Feedback text is required.")]
        [StringLength(500, ErrorMessage = "Feedback text cannot exceed 500 characters.")]
        public string FeedbackText { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a feedback category.")]
        public FeedbackCategory FeedbackCategory { get; set; }

        public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;
    }
}