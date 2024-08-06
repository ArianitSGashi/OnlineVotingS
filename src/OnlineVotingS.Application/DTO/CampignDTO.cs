using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO
{
    public class CampaignDTO
    {
        public int CampaignID { get; set; }
        public int ElectionID { get; set; }
        public int CandidateID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
       // public DateTime UpdatedAt { get; set; }
       // public string ElectionName { get; set; }
       // public string CandidateName { get; set; }
    }
}
