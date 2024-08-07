using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PutDTO;

    public class FeedbackPutDTO
    {
        public int FeedbackID { get; set; }
        public string UserID { get; set; } = null!;
        public int ElectionID { get; set; }
        public string FeedbackText { get; set; } = null!;
        public DateTime FeedbackDate { get; set; }
    }