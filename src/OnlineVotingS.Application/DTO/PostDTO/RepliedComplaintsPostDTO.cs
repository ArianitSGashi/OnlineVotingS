using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.Application.DTO.PostDTO
{
    public class RepliedComplaintsPostDTO
    {
        [Required]
        public int ComplaintID { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The reply text cannot exceed 200 characters.")]
        public string ReplyText { get; set; } = string.Empty;

        public DateTime ReplyDate { get; set; } = DateTime.UtcNow;
    }
}
