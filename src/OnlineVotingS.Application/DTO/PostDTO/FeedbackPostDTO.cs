using OnlineVotingS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PostDTO;

    public class FeedbackPostDTO
    {
        public string FeedbackText { get; set; } = null!;
        public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;
        public FeedbackCategory FeedbackCategory { get; set; }

}