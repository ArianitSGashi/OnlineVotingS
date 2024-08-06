using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO
{
    public class FeedbackDTO
    {
        public int FeedbackID { get; set; }
        public int UserID { get; set; }
        public int ElectionID { get; set; }
        public string FeedbackText { get; set; }
        public DateTime FeedbackDate { get; set; }
        public string ElectionName { get; set; }
    }
}
