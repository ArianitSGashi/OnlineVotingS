using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO
{
    public class ComplaintDTO
    {
        public int ComplaintID { get; set; }
        public int UserID { get; set; }
        public int ElectionID { get; set; }
        public string ComplaintText { get; set; }
        public DateTime ComplaintDate { get; set; }
        public string ElectionName { get; set; }
    }
}
